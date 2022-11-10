using PersonalBlog.Business.Models.Category;
using PersonalBlog.Business.Models.Comment;
using PersonalBlog.Business.Models.Tag;

namespace PersonalBlog.Business.Models.Post.Find;

public class FindPostResponseModel
{
    public Guid Id { get; set; }
    public Guid WriterId { get; set; }
    public string Title { get; init; }
    public string Content { get; init; }
    public List<CategoryModel> Categories { get; init; } = null!;
    public List<TagModel>? Tags { get; init; }
    public List<CommentModel>? Comments { get; init; }
    public int Likes { get; init; }
}