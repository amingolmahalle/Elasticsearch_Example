using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch_Example.Domain;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;

namespace ElasticSearch_Example.Provider
{
    public abstract class ElasticSearchProvider<TEntity, TId> : IElasticSearchProvider<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        private static IConfiguration _configuration;

        private static string _elasticConnectionString;

        protected abstract string IndexName { get; }

        protected abstract string TypeName { get; }

        protected ElasticSearchProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _elasticConnectionString = _configuration["elasticsearch:url"];
        }

        private static ElasticClient _elasticClient;

        public static ElasticClient ElasticClient
        {
            get { return _elasticClient ??= GetElasticClient(); }
        }

        private static ElasticClient GetElasticClient()
        {
            var nodes = _elasticConnectionString.Split(',').Select(p => new Uri(p)).ToArray();
            var connectionPool = new SingleNodeConnectionPool(nodes.First());

            var connectionSettings = new ConnectionSettings(connectionPool)
#if DEBUG
                .EnableDebugMode()
#endif
                .DisableAutomaticProxyDetection()
                .EnableHttpCompression()
                .DisableDirectStreaming()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            connectionSettings.DefaultFieldNameInferrer(p => p);
            // connectionSettings.DefaultIndex("test");// index name should be lowercase

            return new ElasticClient(connectionSettings);
        }

        public virtual async Task<IndexResponse> AddDocumentAsync(TEntity entity)
        {
          return await ElasticClient.IndexAsync(entity, idx => idx.Index(IndexName));
        }

        public virtual async Task<IndexResponse> EditDocumentAsync(TEntity entity)
        {
            var isExistResult = await IsExistDocumentAsync(entity.Id);

            if (!isExistResult)
            {
                throw new InvalidOperationException("not found");
            }

            return await ElasticClient.IndexAsync(entity, idx => idx.Index(IndexName));
        }

        public virtual async Task<DeleteResponse> DeleteDocumentAsync(TId id)
        {
            return await ElasticClient.DeleteAsync<TEntity>(id.ToString(), i => i.Index(IndexName));

            // await ElasticClient.DeleteByQueryAsync<TEntity>(q => q.MatchAll());
        }

        public virtual async Task<List<TEntity>> SearchDocumentByQueryAsync(string query, int page, int pageSize)
        {
            var response = await ElasticClient.SearchAsync<TEntity>(
                s =>
                    s.Query(q => q.QueryString(d => d.Query(query)))
                        .Index(IndexName)
                        .From((page - 1) * pageSize)
                        .Size(pageSize));

            if (!response.IsValid)
            {
                Console.WriteLine(
                    $"an error has occured when find data from elasticsearch --> {response.ServerError.Error}");

                throw new Exception(
                    $"an error has occured when find data from elasticsearch --> {response.ServerError.Error}");
            }

            return response.Documents.ToList();
        }

        public virtual async Task ReIndexAsync(IEnumerable<TEntity> entities)
        {
            if (await IsExistIndexAsync())
            {
                await DeleteIndexAsync(IndexName);
            }

            await CreateIndexAsync();

            await AddManyDocumentAsync(entities);
        }

        public virtual async Task DeleteIndexAsync(string aliasName)
        {
            await ElasticClient.Indices.DeleteAsync(aliasName);
        }

        public virtual async Task<bool> IsExistIndexAsync()
        {
            return (await ElasticClient.Indices.ExistsAsync(IndexName)).Exists;
        }

        public virtual async Task<bool> IsExistDocumentAsync(TId id)
        {
            return (await ElasticClient.DocumentExistsAsync<TEntity>(id.ToString(), p => p.Index(IndexName))).Exists;
        }

        public virtual async Task IndexBulkAsync(IEnumerable<TEntity> entities)
        {
            await ElasticClient
                .BulkAsync(b =>
                    b.Index(IndexName)
                        .IndexMany(entities));
        }

        public virtual async Task<BulkResponse> AddManyDocumentAsync(IEnumerable<TEntity> entities)
        {
            return await ElasticClient.IndexManyAsync(entities, IndexName);
        }

        public virtual async Task CreateIndexAsync()
        {
            await ElasticClient
                .Indices
                .CreateAsync(IndexName, index => index.Map<TEntity>(x => x.AutoMap()));
        }

        public virtual void CreateIndex()
        {
            ElasticClient
                .Indices
                .Create(IndexName, index => index.Map<TEntity>(x => x.AutoMap()));
        }
    }
}