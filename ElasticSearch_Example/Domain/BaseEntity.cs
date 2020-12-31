namespace ElasticSearch_Example.Domain
{
    public class BaseEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }

    public interface IEntity
    {
    }
}