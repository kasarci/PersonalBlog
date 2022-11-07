using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using Polly;
using Polly.Retry;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract class RepositoryBaseCommon<T>
where T : IEntity, new()
{
    private readonly string _databaseName;
    private AsyncRetryPolicy _asyncRetryPolicy;
    private string CollectionName { get; init; } = typeof(T).Name.ToLower() + "s";
    protected readonly IMongoCollection<T> _collection;
    protected readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;
    protected readonly UpdateDefinitionBuilder<T> _updateBuilder = Builders<T>.Update;
    protected readonly SortDefinitionBuilder<T> _sortDefinition = Builders<T>.Sort;

    protected RepositoryBaseCommon(IMongoClient mongoClient, IConfiguration configuration)
    {
        _databaseName = configuration.GetSection("MongoDbSettings:DatabaseName").Value;
        IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
        _collection = database.GetCollection<T>(CollectionName);
    }

    /// <summary>
    /// retry operation for three times if IOException occurs
    /// </summary>
    /// <typeparam name="TResult">return type</typeparam>
    /// <param name="action">action</param>
    /// <returns>action result</returns>
    /// <example>
    /// return Retry(() => 
    /// { 
    ///     do_something;
    ///     return something;
    /// });
    /// </example>
    protected virtual Task<TResult> RetryAsync<TResult>(Func<Task<TResult>> action)
    {
        return (_asyncRetryPolicy ??= GetPolicyBuilder().RetryAsync(3)).ExecuteAsync(action);
    }

    /// <summary>
    /// retry operation for three times if IOException occurs
    /// </summary>
    /// <param name="action">action</param>
    /// <returns>action result</returns>
    /// <example>
    /// return Retry(() =>
    /// {
    ///     do_something;
    ///     return something;
    /// });
    /// </example>
    protected virtual Task RetryAsync(Func<Task> action)
    {
        return (_asyncRetryPolicy ??= GetPolicyBuilder().RetryAsync(3)).ExecuteAsync(action);
    }

    /// <summary>
    /// Get retry policy
    /// </summary>
    /// <returns></returns>
    protected PolicyBuilder GetPolicyBuilder()
    {
        return RetryPolicy.Handle<MongoConnectionException>(
            i => i.InnerException?.GetType() == typeof(IOException) ||
            i.InnerException?.GetType() == typeof(SocketException));
    }
}