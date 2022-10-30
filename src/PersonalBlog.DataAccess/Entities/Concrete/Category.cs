using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Entities.Concrete
{
    public record Category : BlogItemBase, IBlogItem, IEntity
    {
        public string Name { get; init; } = null!;
    }
}