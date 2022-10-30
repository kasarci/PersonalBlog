using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

public interface IRepositoryCommon<T, TKey> where T : IEntity<TKey>, new() where TKey : IEquatable<TKey>
{
    IMongoCollection<T> Collection { get; }
    FilterDefinitionBuilder<T> FilterDefinitionBuilder { get; }
    UpdateDefinitionBuilder<T> UpdateDefinitionBuilder { get; }
    string CollectionName { get; }
}