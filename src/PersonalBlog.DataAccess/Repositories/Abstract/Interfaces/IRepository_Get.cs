using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for First
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryGet<T, TKey> where T : IEntity<TKey>, new() where TKey : IEquatable<TKey>, IRepositoryCommon<T,TKey>
{
    /// <summary>
    /// Get entity by Id.
    /// </summary>
    /// <param name="id"><typeparamref name="T"/> Id</param>
    /// <returns>entity of <typeparamref name="T"/></returns>
    Task<T> GetAsync(TKey id);
}