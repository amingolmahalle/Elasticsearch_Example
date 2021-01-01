using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch_Example.Domain;

namespace ElasticSearch_Example.Repository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostAllAsync();
        
        Task<IEnumerable<Post>> GetPostAllPaginationAsync(int count, int skip = 0);

        Task<bool> AddPostAsync(Post post);

        Task<bool> EditPostAsync(Post post);

        Task<bool> DeletePostAsync(Guid postId);

        Task<List<Post>> SearchPostByQueryAsync(string query, int page, int pageSize);

        Task ReIndexAsync(IEnumerable<Post> posts);

        Task<Post> GetPostByIdAsync(Guid postId);

        Task CreateIndexAsync();
    }
}