using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch_Example.Domain;
using ElasticSearch_Example.Models;
using ElasticSearch_Example.Repository;

namespace ElasticSearch_Example.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetPostAllPaginationAsync(int count, int skip = 0)
        {
            return await _postRepository.GetPostAllPaginationAsync(count, skip);
        }

        public async Task AddPostAsync(AddPostRequest request)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Slug = request.Slug,
                PublishDate = DateTime.Now,
                LastModified = DateTime.Now,
                Excerpt = request.Excerpt,
                Content = request.Content,
                IsPublished = true,
                Categories = request.Categories,
                Comments = request
                    .Comments
                    .Select(
                        c => new Comment
                        {
                            Id = new Random(DateTime.Now.Millisecond).Next(0, 99999),
                            Author = c.Author,
                            Content = c.Content,
                            Email = c.Email,
                            PublishDate = DateTime.Now
                        })
            };

            var result = await _postRepository.AddPostAsync(post);

            if (!result)
            {
                throw new InvalidOperationException();
            }
        }

        public async Task EditPostAsync(Guid postId, EditPostRequest request)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);

            if (post != null)
            {
                post.Id = postId;
                post.Title = request.Title;
                post.Slug = request.Slug;
                post.PublishDate = DateTime.Now;
                post.LastModified = DateTime.Now;
                post.Excerpt = request.Excerpt;
                post.Content = request.Content;
                post.IsPublished = request.IsPublished;
                post.Categories = request.Categories;
                post.Comments = request
                    .Comments
                    .Select(
                        c => new Comment
                        {
                            Id = c.Id,
                            Author = c.Author,
                            Content = c.Content,
                            Email = c.Email,
                            PublishDate = DateTime.Now
                        });

                var result = await _postRepository.EditPostAsync(post);

                if (!result)
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                throw new InvalidDataException("not found");
            }
        }

        public async Task DeletePostAsync(DeletePostRequest request)
        {
            var result = await _postRepository.DeletePostAsync(request.Id);

            if (!result)
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<List<Post>> SearchPostByQueryAsync(PostSearchRequest request)
        {
            return await _postRepository.SearchPostByQueryAsync(request.Query, request.Page, request.PageSize);
        }

        public async Task ReIndexAsync()
        {
            var posts = await _postRepository.GetPostAllAsync();

            await _postRepository.ReIndexAsync(posts);
        }

        public async Task CreateIndexAsync()
        {
            await _postRepository.CreateIndexAsync();
        }
    }
}