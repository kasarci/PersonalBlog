using System.Linq.Expressions;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for Find
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryFind<T> where T : IEntity, new()
{
    /// <summary>
    /// Find documents with filter.
    /// </summary>
    /// <param name="filter">MongoDb.Driver's FilterDefinition</param>
    /// <returns>collection of <typeparamref name="T"/></returns>
    Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter);

    /// <summary>
    /// Find documents with filter.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <returns>collection of <typeparamref name="T"/></returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> filter);

    /// <summary>
    /// Find entities with paging.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <param name="pageIndex">Page index, based on 0</param>
    /// <param name="size">Number of items in page</param>
    /// <returns>collection of <typeparamref name="T"/></returns>
    Task<IEnumerable<T>> FindAsyncWithPagination(Expression<Func<T,bool>> filter, int pageIndex, int size);

    /// <summary>
    /// Find entities with paging and order.
    /// Default order is descending.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <param name="order">Expression for order</param>
    /// <param name="pageIndex">Page index, based on 0</param>
    /// <param name="size">Number of items in page</param>
    /// <returns>collection of <typeparamref name="T"/></returns>
    Task<IEnumerable<T>> FindAsyncWithPagination(Expression<Func<T,bool>> filter, Expression<Func<T,object>> order, int pageIndex, int size);

    /// <summary>
    /// Find entities with paging and order.
    /// Default order is descending.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <param name="order">Expression for order</param>
    /// <param name="pageIndex">Page index, based on 0</param>
    /// <param name="size">Number of items in page</param>
    /// <param name="isDescending">ordering direction</param>
    /// <returns>collection of <typeparamref name="T"/></returns>
    Task<IEnumerable<T>> FindAsyncWithPagination(Expression<Func<T,bool>> filter, Expression<Func<T,object>> order, int pageIndex, int size, bool isDescending);
}