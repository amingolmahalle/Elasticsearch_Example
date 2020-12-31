using System;

namespace ElasticSearch_Example.Domain
{
    public class Comment : BaseEntity<int>
    {
        public string Author { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
    }
}