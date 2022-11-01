namespace PersonalBlog.Business.Models.Post.Update;

public class UpdatePostRequestModel
{
    public Guid Id { get; set; }
    public string Title{ get; set; }
    public string Content { get; set; }
    public List<Guid> CategoryIds { get; set; }
    public List<Guid>? TagIds { get; set; }
}