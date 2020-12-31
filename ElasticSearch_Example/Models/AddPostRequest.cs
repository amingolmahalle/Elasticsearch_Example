using System.Collections.Generic;

namespace ElasticSearch_Example.Models
{
    public class AddPostRequest
    {
        public AddPostRequest()
        {
            Comments = new List<AddCommentDto>();
        }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public IList<string> Categories { get; set; }

        public IEnumerable<AddCommentDto> Comments { get; set; }
    }

    public class AddCommentDto
    {
        public string Author { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }
    }
}