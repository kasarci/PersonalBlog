using System.Linq.Expressions;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for FindAll
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryFindAll<T, TKey> where T : IEntity<TKey>, new() where TKey : IEquatable<TKey>, IRepositoryCommon<T,TKey>
{
    /// <summary>
    /// Fetch all entities.
    /// </summary>
    /// <returns>collection of <typeparamref name="T"/></returns>
    Task<IEnumerable<T>> FindAllAsync();

    /// <summary>
    /// Fetch all entities with pagination and order.
    /// </summary>
    /// <param name="order">Expression for order</param>
    /// <param name="pageIndex">Page index, based on 0</param>
    /// <param name="size">Number of items in page</param>
    /// <returns>collection of <typeparamref name="T"/></returns>
    Task<IEnumerable<T>> FindAllAsyncWithPagination(Expression<Func<T, object>> order, int pageIndex, int size);

    /// <summary>
    /// Fetch all entities with pagination and order.
    /// </summary>
    /// <param name="order">Expression for order</param>
    /// <param name="pageIndex">Page index, based on 0</param>
    /// <param name="size">Number of items in page</param>
    /// <param name="isDescending">Ordering direction</param>
    /// <returns>collection of <typeparamref name="T"/></returns>
    Task<IEnumerable<T>> FindAllAsyncWithPagination(Expression<Func<T, object>> order, int pageIndex, int size, bool isDescending);
}