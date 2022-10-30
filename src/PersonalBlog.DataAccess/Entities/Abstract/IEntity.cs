namespace PersonalBlog.DataAccess.Entities.Abstract;

public interface IEntity
{
    Guid Id { get; init; }
    DateTime CreatedAt { get; init; }
    DateTime? ModifiedAt { get; set; }
}