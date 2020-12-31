using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearch_Example.Provider
{
    public interface IElasticSearchProvider<TEntity, in TId>
    {
        Task AddDocumentAsync(TEntity entity);

        Task EditDocumentAsync(TEntity entity);

        Task<DeleteResponse> DeleteDocumentAsync(TId id);

        Task DeleteIndexAsync(string aliasName);

        Task<List<TEntity>> SearchByQueryAsync(string query, int page, int pageSize);

        Task ReIndexAsync(IEnumerable<TEntity> entities);

        Task CreateIndexAsync();

        void CreateIndex();

        Task IndexBulkAsync(IEnumerable<TEntity> entities);

        Task AddManyDocumentAsync(IEnumerable<TEntity> entities);

        Task<bool> IsExistIndexAsync();

        Task<bool> IsExistDocumentAsync(TId id);
    }
}