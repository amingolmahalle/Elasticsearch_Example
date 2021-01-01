using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearch_Example.Provider
{
    public interface IElasticSearchProvider<TEntity, in TId>
    {
        Task<IndexResponse> AddDocumentAsync(TEntity entity);

        Task<IndexResponse> EditDocumentAsync(TEntity entity);

        Task<DeleteResponse> DeleteDocumentAsync(TId id);

        Task DeleteIndexAsync(string aliasName);

        Task<List<TEntity>> SearchDocumentByQueryAsync(string query, int page, int pageSize);

        Task ReIndexAsync(IEnumerable<TEntity> entities);

        Task CreateIndexAsync();

        void CreateIndex();

        Task IndexBulkAsync(IEnumerable<TEntity> entities);

        Task<BulkResponse> AddManyDocumentAsync(IEnumerable<TEntity> entities);

        Task<bool> IsExistIndexAsync();

        Task<bool> IsExistDocumentAsync(TId id);
    }
}