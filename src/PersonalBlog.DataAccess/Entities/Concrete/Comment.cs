using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Entities.Concrete
{
    public record Comment : BlogItemBase, IBlogItem
    {
        public Guid PostId { get; init; }
        public string UserName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Content { get; init; } = null!;
    }
}