using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch_Example.Domain;
using ElasticSearch_Example.Models;

namespace ElasticSearch_Example.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostAllPaginationAsync(int count, int skip = 0);

        Task AddPostAsync(AddPostRequest request);

        Task EditPostAsync(Guid postId, EditPostRequest request);

        Task DeletePostAsync(DeletePostRequest post);

        Task<List<Post>> SearchPostByQueryAsync(PostSearchRequest request);

        Task ReIndexAsync();

        Task CreateIndexAsync();
    }
}