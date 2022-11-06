using FluentValidation;
using MongoDB.Driver;
using PersonalBlog.Business.Models.Validations;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.Business.Services.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using PersonalBlog.DataAccess.Repositories.Concrete;

namespace PersonalBlog.API.Extensions;

public static class ServiceExtensions
{
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ITagService, TagService>();
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IValidationsMarker>();
    }
}