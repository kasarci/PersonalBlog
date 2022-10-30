using System.Linq.Expressions;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

public interface IRepositoryUtils<T> where T : IEntity, new()
{
    /// <summary>
    /// get number of filtered entities.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    Task<long> CountAsync(Expression<Func<T, bool>> filter);

    /// <summary>
    /// validate if filter result exists
    /// </summary>
    /// <param name="filter">expression filter</param>
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
}