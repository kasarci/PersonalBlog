using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Entities.Concrete;

public record Post : BlogItemBase, IBlogItem
{
    public Guid WriterId { get; set; }
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public List<Category> Categories { get; init; } = null!;
    public List<Tag>? Tags { get; init; }
    public List<Comment>? Comments { get; init; }
    public int Likes { get; init; }
}