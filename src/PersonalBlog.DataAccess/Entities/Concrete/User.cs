using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Entities.Concrete;

//TODO: Identity features will be added.
public record User : IEntity
{
    public Guid Id { get; init; }
    public List<Post>? Posts { get; init; }
    public DateTime CreatedAt { get ; init ; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
}