namespace PersonalBlog.Business.Models.Post.Add;

public class AddPostRequestModel
{
    public string WriterId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Guid> CategoryIds { get; set; }
    public List<Guid>? TagIds { get; set; }
}