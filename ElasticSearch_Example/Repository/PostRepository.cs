using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch_Example.Domain;
using ElasticSearch_Example.Provider;
using Microsoft.Extensions.Configuration;

namespace ElasticSearch_Example.Repository
{
    public class PostRepository : ElasticSearchProvider<Post, Guid>, IPostRepository
    {
        protected override string IndexName { get; } = "posts"; //index name should be lowercase

        protected override string TypeName { get; } = "post";

        private static List<Post> _cache = new List<Post>();

        static PostRepository()
        {
            LoadPosts();
        }

        public PostRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Post>> GetPostAllPaginationAsync(int count, int skip = 0)
        {
            var posts = _cache
                .Where(p => p.PublishDate <= DateTime.UtcNow && p.IsPublished)
                .Skip(skip)
                .Take(count);

            return posts;
        }

        public async Task<IEnumerable<Post>> GetPostAllAsync()
        {
            return _cache.Where(p => p.PublishDate <= DateTime.UtcNow && p.IsPublished);
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return _cache.Single(p => p.Id == postId);
        }

        public async Task<bool> AddPostAsync(Post post)
        {
            var response = await base.AddDocumentAsync(post);

            if (response.IsValid)
            {
                AddDataToInMemory(post);
            }

            return response.IsValid;
        }

        public async Task<bool> EditPostAsync(Post post)
        {
            var response = await base.EditDocumentAsync(post);

            if (response.IsValid)
            {
                EditDataFromInMemory(post);
            }

            return response.IsValid;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var response = await base.DeleteDocumentAsync(postId);

            if (response.IsValid)
            {
                RemoveDataFromInMemory(postId);
            }

            return response.IsValid;
        }

        public async Task<List<Post>> SearchPostByQueryAsync(string query, int page, int pageSize)
        {
            return await base.SearchDocumentByQueryAsync(query, page, pageSize);
        }
        
        public override async Task ReIndexAsync(IEnumerable<Post> entities)
        {
            _cache = new List<Post>();

            await base.ReIndexAsync(entities);
            
            AddRangeDataToInMemory(entities);
        }

        #region InMemoryData

        private static void AddRangeDataToInMemory(IEnumerable<Post> posts)
        {
            _cache.AddRange(posts);

            SortCache();
        }

        private static void AddDataToInMemory(Post post)
        {
            _cache.Add(post);

            SortCache();
        }

        private static List<Post> GetDataFromInMemory()
        {
            return _cache;
        }

        private static void RemoveDataFromInMemory(Guid postId)
        {
            if (_cache.Any(p => p.Id == postId))
            {
                var itemToRemove = _cache.SingleOrDefault(c => c.Id == postId);

                _cache.Remove(itemToRemove);
            }
        }

        private static void EditDataFromInMemory(Post post)
        {
            RemoveDataFromInMemory(post.Id);
            AddDataToInMemory(post);

            SortCache();
        }

        private static void SortCache()
        {
            GetDataFromInMemory().Sort((p1, p2) => p2.PublishDate.CompareTo(p1.PublishDate));
        }

        private static void LoadPosts()
        {
            var posts = new List<Post>
            {
                new Post
                {
                    Id = Guid.Parse("0C885DD3-7DD9-484B-9B20-3E6552BCA144"),
                    Title = "title1",
                    Excerpt = "excerpt1",
                    Content = "content1",
                    Slug = "slug1",
                    PublishDate = DateTime.Now.AddHours(-5),
                    LastModified = DateTime.Now,
                    IsPublished = true,
                    Categories = new List<string>
                    {
                        "Book",
                        "Game",
                        "Cosmetics"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Id = 100,
                            Author = "amin golmahalle",
                            Email = "amin.golmahalle@gmail.com",
                            Content = "Description about amin Book",
                            PublishDate = DateTime.Now.AddYears(-1).AddMonths(-2).AddDays(-3)
                        },
                        new Comment
                        {
                            Id = 200,
                            Author = "reza jenabi",
                            Email = "reza.jenabi@gmail.com",
                            Content = "Description about reza Book",
                            PublishDate = DateTime.Now.AddYears(-1).AddMonths(-6).AddDays(-12)
                        },
                        new Comment
                        {
                            Id = 300,
                            Author = "hamed naeemaei",
                            Email = "hamed.naemaei@gmail.com",
                            Content = "Description about hamed Book",
                            PublishDate = DateTime.Now.AddYears(-1).AddMonths(-8).AddDays(-7)
                        }
                    }
                },
                new Post
                {
                    Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Title = "title2",
                    Excerpt = "excerpt2",
                    Content = "content2",
                    Slug = "slug2",
                    PublishDate = DateTime.Now.AddHours(-5),
                    LastModified = DateTime.Now,
                    IsPublished = true,
                    Categories = new List<string>
                    {
                        "Clothing",
                        "Food",
                        "Decoration",
                        "Sports",
                        "Digital"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Id = 100,
                            Author = "javad khorasani",
                            Email = "javad.khorasani@gmail.com",
                            Content = "Description about javad Book",
                            PublishDate = DateTime.Now.AddYears(-2).AddMonths(-8).AddDays(-9)
                        },
                        new Comment
                        {
                            Id = 200,
                            Author = "mohsen khazan",
                            Email = "mohsen.khazan@gmail.com",
                            Content = "Description about mohsen Book",
                            PublishDate = DateTime.Now.AddYears(-1).AddMonths(-11).AddDays(-2)
                        }
                    }
                }
            };

            AddRangeDataToInMemory(posts);
        }

        #endregion
    }
}