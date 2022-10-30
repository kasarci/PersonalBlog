using System.Linq.Expressions;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for Delete
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryDelete<T> where T : IEntity, new()
{
    /// <summary>
    /// Delete by Id.
    /// </summary>
    /// <param name="id"></param>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Delete by entity.
    /// </summary>
    /// <param name="Entity">Entity</param>
    Task<bool> DeleteAsync(T entity);

    /// <summary>
    /// Delete filtered entities.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    Task<bool> DeleteAsync(Expression<Func<T,bool>> filter);

    /// <summary>
    /// Delete all entities.
    /// </summary>
    Task<bool> DeleteAllAsync();
}