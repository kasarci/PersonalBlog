using MongoDB.Bson.Serialization.Attributes;

namespace PersonalBlog.DataAccess.Entities.Abstract;

public interface IEntity
{
    [BsonId]
    Guid Id { get; init; }
    DateTimeOffset CreatedAt { get; init; }
    DateTimeOffset? ModifiedAt { get; set; }
}