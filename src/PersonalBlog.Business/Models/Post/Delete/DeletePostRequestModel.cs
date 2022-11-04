namespace PersonalBlog.Business.Models.Post.Delete;

public class DeletePostRequestModel
{
    public Guid PostId { get; set; }
    /// <summary>
    /// By default, deleting the post just hides the content from the user.
    /// Set DeleteCompletely to true if you want to delete the post from the database as well.
    /// </summary>
    /// <value></value>
    public bool DeleteCompletely { get; set; } = false;
}