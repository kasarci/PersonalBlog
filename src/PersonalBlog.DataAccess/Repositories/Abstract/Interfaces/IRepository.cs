using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

/// <summary>
/// Mongo Repository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T: IEntity,
IRepositoryDelete<T>,
IRepositoryFind<T>,
IRepositoryFindAll<T>,
IRepositoryFirst<T>,
IRepositoryGet<T>,
IRepositoryAdd<T>,
IRepositoryUpdate<T>,
IRepositoryUtils<T>,
new()
{ }