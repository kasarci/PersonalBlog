using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for Add
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryAdd<T, TKey> where T : IEntity<TKey>, new() where TKey : IEquatable<TKey>, IRepositoryCommon<T,TKey>
{
    /// <summary>
    /// Add entity.
    /// </summary>
    /// <param name="entity">entity of <typeparamref name="T"/></param>
    Task AddOneAsync(T entity);

    /// <summary>
    /// Add entities.
    /// </summary>
    /// <param name="entities">collection of <typeparamref name="T"/></param>
    Task AddManyAsync(IEnumerable<T> entities);
}