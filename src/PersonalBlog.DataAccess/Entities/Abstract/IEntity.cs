namespace PersonalBlog.DataAccess.Entities.Abstract;

public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; init; }
}
public interface IEntity : IEntity<Guid> { }