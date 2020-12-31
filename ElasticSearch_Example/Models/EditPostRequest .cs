using System;
using System.Collections.Generic;

namespace ElasticSearch_Example.Models
{
    public class EditPostRequest
    {
        public EditPostRequest()
        {
            Comments = new List<EditCommentDto>();
        }
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public bool IsPublished { get; set; } = true;

        public IList<string> Categories { get; set; }

        public IEnumerable<EditCommentDto> Comments { get; set; }
    }

    public class EditCommentDto
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }
    }
}