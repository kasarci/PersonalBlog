using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T, TKey> where T : IEntity<TKey>, new() where TKey : IEquatable<TKey>,
IRepositoryCommon<T,TKey>,
IRepositoryDelete<T,TKey>,
IRepositoryFind<T,TKey>,
IRepositoryFindAll<T,TKey>,
IRepositoryFirst<T,TKey>,
IRepositoryGet<T,TKey>,
IRepositoryAdd<T,TKey>,
IRepositoryUpdate<T,TKey>,
IRepositoryUtils<T,TKey>
{ }