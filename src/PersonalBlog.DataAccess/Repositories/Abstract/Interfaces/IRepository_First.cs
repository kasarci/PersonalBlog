using System.Linq.Expressions;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for First
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryFirst<T> where T : IEntity, new()
{
    /// <summary>
    /// Returns the firs entity in collection.
    /// </summary>
    /// <returns>entity of <typeparamref name="T"/></returns>
    Task<T> FirstAsync();

    /// <summary>
    /// Returns the firs entity in filtered collection.
    /// </summary>
    /// <param name="filter">MongoDb.Driver's FilterDefinition</param>
    /// <returns>entity of <typeparamref name="T"/></returns>
    Task<T> FirstAsync(FilterDefinition<T> filter);

    /// <summary>
    /// Returns the firs entity in filtered collection.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <returns>entity of <typeparamref name="T"/></returns>
    Task<T> FirstAsync(Expression<Func<T,bool>> filter);

    /// <summary>
    /// Returns the firs entity in filtered and ordered collection.
    /// Default order is descending.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <param name="order">Expression for order</param>
    /// <returns>entity of <typeparamref name="T"/></returns>
    Task<T> FirstAsync(Expression<Func<T,bool>> filter, Expression<Func<T,object>> order);

    /// <summary>
    /// Returns the firs entity in filtered and ordered collection.
    /// Default order is descending.
    /// </summary>
    /// <param name="filter">Expression for filter</param>
    /// <param name="order">Expression for order</param>
    /// <param name="isDescending">Ordering direction</param>
    /// <returns>entity of <typeparamref name="T"/></returns>
    Task<T> FirstAsync(Expression<Func<T,bool>> filter, Expression<Func<T,object>> order, bool isDescending);
}