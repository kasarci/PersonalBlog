using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

public interface IRepositoryCommon<T> where T : IEntity, new()
{
    IMongoCollection<T> Collection { get; }
    FilterDefinitionBuilder<T> FilterDefinitionBuilder { get; }
    UpdateDefinitionBuilder<T> UpdateDefinitionBuilder { get; }
    string CollectionName { get; }
}