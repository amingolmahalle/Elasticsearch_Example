using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch_Example.Domain;
using ElasticSearch_Example.Models;
using ElasticSearch_Example.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch_Example.Controllers
{
    [Route("post")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("searchDocument")]
        public async Task<List<Post>> Search([FromBody] PostSearchRequest request)
        {
            return await _postService.SearchPostByQueryAsync(request);
        }

        [HttpPost("addDocument")]
        public async Task AddDocument([FromBody] AddPostRequest request)
        {
            await _postService.AddPostAsync(request);
        }

        [HttpPut("editDocument/{postId}")]
        public async Task EditDocument(Guid postId, [FromBody] EditPostRequest request)
        {
            await _postService.EditPostAsync(postId, request);
        }

        [HttpDelete("deleteDocument/{Id}")]
        public async Task Delete([FromRoute] DeletePostRequest request)
        {
            await _postService.DeletePostAsync(request);
        }

        [HttpGet("reIndex")]
        public async Task ReIndex()
        {
            await _postService.ReIndexAsync();
        }

        [HttpGet("createIndex")]
        public async Task CreateIndex()
        {
            await _postService.CreateIndexAsync();
        }
    }
}