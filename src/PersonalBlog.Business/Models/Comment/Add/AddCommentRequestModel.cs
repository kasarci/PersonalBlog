namespace PersonalBlog.Business.Models.Comment.Add;

public class AddCommentRequestModel
{
    public Guid PostId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Content { get; set; }
}