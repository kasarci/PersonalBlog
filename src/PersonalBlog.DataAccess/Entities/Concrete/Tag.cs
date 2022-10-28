using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Entities.Concrete
{
    public record Tag : BlogItemBase, IBlogItem
    {
        public string Name { get; init; } = null!;
    }
}