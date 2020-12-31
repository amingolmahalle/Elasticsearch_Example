using System;
using System.Collections.Generic;

namespace ElasticSearch_Example.Domain
{
    public class Post : BaseEntity<Guid>
    {
        public Post()
        {
            Categories = new List<string>();
            Comments = new List<Comment>();
        }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsPublished { get; set; } = true;

        public IList<string> Categories { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}