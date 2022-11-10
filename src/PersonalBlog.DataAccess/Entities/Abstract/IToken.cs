namespace PersonalBlog.DataAccess.Entities.Abstract;

public interface IToken : IEntity
{
    public string Token { get; init; }
    public DateTimeOffset ExpiryDate { get; init; }
}