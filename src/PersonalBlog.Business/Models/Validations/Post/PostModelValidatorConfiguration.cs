namespace PersonalBlog.Business.Models.Validations.Post;

public static class PostModelValidatorConfiguration
{
    public const int MinimumTitleLength = 5;

    public const int MaximumTitleLength = 50; 
    public const int MinimumContentLength = 5;
    public const int MaximumContentLength = Int32.MaxValue;
}