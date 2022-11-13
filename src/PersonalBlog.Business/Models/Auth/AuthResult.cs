namespace PersonalBlog.Business.Models.Auth;

public class AuthResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool Result { get; set; } = false;
    public List<string> Errors { get; set; }
}