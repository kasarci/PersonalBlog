using System.Linq.Expressions;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for Add
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryUpdate<T, TKey> where T : IEntity<TKey>, new() where TKey : IEquatable<TKey>, IRepositoryCommon<T,TKey>
{
    /// <summary>
    /// Replace an existing entity.
    /// </summary>
    /// <param name="entity">entity of <typeparamref name="T"/></param>
    Task<bool> UpdateOneAsync(T entity);

    /// <summary>
    /// Replace existing entities.
    /// </summary>
    /// <param name="entities">collection of <typeparamref name="T"/></param>
    Task<bool> UpdateManyAsync(IEnumerable<T> entities);

    /// <summary>
    /// Updates the desired fields of the entity with Id value.
    /// </summary>
    /// <param name="id"><typeparamref name="T"/> Id</param>
    /// <param name="updateDefinitions">updated field(s)</param>
    Task<bool> UpdateOneAsync(TKey id, params UpdateDefinition<T>[] updateDefinitions);

    /// <summary>
    /// Updates the desired fields of the entity with entity value.
    /// </summary>
    /// <param name="entity">entity of <typeparamref name="T"/></param>
    /// <param name="updateDefinitions">updated field(s)</param>
    Task<bool> UpdateOneAsync(T entity, params UpdateDefinition<T>[] updateDefinitions);

    /// <summary>
    /// Find and update an entity.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <param name="updateDefinition">MongoDb.Driver's UpdateDefinition</param>
    /// <param name="options">MongoDb.Driver's FindOneAndUpdateOptions</param>
    Task<bool> FindOneAndUpdateAsync(Expression<Func<T>> filter, UpdateDefinition<T> updateDefinition, FindOneAndUpdateOptions<T>? options);

    /// <summary>
    /// Find and update an entity.
    /// </summary>
    /// <param name="filter">MongoDb.Driver's FilterDefinition</param>
    /// <param name="updateDefinition">MongoDb.Driver's UpdateDefinition</param>
    /// <param name="options">MongoDb.Driver's FindOneAndUpdateOptions</param>
    Task<bool> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, FindOneAndUpdateOptions<T>? options);

    /// <summary>
    /// update found entities by filter with updated fields
    /// </summary>
    /// <param name="filter">MongoDb.Driver's FilterDefinition</param>
    /// <param name="updateDefinitions">MongoDb.Driver's UpdateDefinition</param>
    Task<bool> UpdateManyAsync(FilterDefinition<T> filter, params UpdateDefinition<T>[] updateDefinitions);

    /// <summary>
    /// update found entities by filter with updated fields
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <param name="updateDefinitions">MongoDb.Driver's UpdateDefinition</param>
    Task<bool> UpdateManyAsync(Expression<Func<T, bool>> filter, params UpdateDefinition<T>[] updateDefinitions);
}