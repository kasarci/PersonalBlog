using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for Add
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryAdd<T> where T : IEntity, new()
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