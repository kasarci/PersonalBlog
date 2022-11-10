using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Entities.Concrete;

public record RefreshToken : IToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; init; }
    public string UserName { get; init; }
    public string Token { get; init; }
    public string JwtId { get; init; }
    public bool IsUsed { get; init; }
    public bool IsRevoked { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset ExpiryDate { get; init; }
    public DateTimeOffset? ModifiedAt { get; set; }
}