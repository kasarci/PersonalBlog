namespace PersonalBlog.Business.Models.Validations.Comment;

public static class CommentModelValidatorConfiguration
{
    public const int MinimumCommentLength = 2;
    public const int MaximumCommentLength = 500;
    public const int MinimumUsernameLength = 2;
    public const int MaximumUsernameLength = 500;
}