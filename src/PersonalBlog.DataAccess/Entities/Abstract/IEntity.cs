using MongoDB.Bson.Serialization.Attributes;

namespace PersonalBlog.DataAccess.Entities.Abstract;

public interface IEntity
{
    [BsonId]
    Guid Id { get; init; }
    DateTime CreatedAt { get; init; }
    DateTime? ModifiedAt { get; set; }
}