namespace PersonalBlog.Business.Models.Post.Add;

public class AddCommentReferenceRequestModel
{
    public Guid PostId { get; set; }
    public Guid CommentId { get; set; }
}