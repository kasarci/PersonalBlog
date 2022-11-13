namespace PersonalBlog.Business.Models.User;

public class TokenRequestModel
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}