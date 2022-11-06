namespace PersonalBlog.Business.Models.Validations.Comment;

internal static class CommentModelValidatorConfiguration
{
    internal const int MinimumCommentLength = 2;
    internal const int MaximumCommentLength = 500;
    internal const int MinimumUsernameLength = 2;
    internal const int MaximumUsernameLength = 500;
}