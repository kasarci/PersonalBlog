using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface for First
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryGet<T> where T : IEntity, new()
{
    /// <summary>
    /// Get entity by Id.
    /// </summary>
    /// <param name="id"><typeparamref name="T"/> Id</param>
    /// <returns>entity of <typeparamref name="T"/></returns>
    Task<T> GetAsync(Guid id);
}