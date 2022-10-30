namespace PersonalBlog.DataAccess.Entities.Abstract;

public interface IBlogItem: IEntity
{
    bool IsActive { get; init; }
}