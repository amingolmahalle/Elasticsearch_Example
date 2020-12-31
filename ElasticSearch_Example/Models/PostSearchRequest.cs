namespace ElasticSearch_Example.Models
{
    public class PostSearchRequest
    {
        public string Query { get; set; }
        
        public int Page { get; set; } = 1;
        
        public int PageSize { get; set; } = 5;
    }
}