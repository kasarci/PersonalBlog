using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Entities.Concrete;

//TODO: Identity features will be added.
public record User : IEntity
{
    public Guid Id { get; set; }
    public List<Post>? Posts { get; init; }
    public DateTimeOffset CreatedAt { get ; init ; } = DateTime.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
}