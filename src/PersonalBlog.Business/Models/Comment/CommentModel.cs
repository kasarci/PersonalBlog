namespace PersonalBlog.Business.Models.Comment;

public class CommentModel
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Content { get; set; }
}