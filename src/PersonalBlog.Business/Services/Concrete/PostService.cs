using System.Linq.Expressions;
using AutoMapper;
using PersonalBlog.Business.Models.Category.Find;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.Business.Models.Post.Update;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Razor;

namespace PersonalBlog.Business.Services.Concrete;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;


    public PostService(IPostRepository repository, IMapper mapper, ICategoryRepository categoryRepository, ITagRepository tagRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }

    public async Task<AddPostResponseModel> AddAsync(AddPostRequestModel addPostRequestModel)
    {
        var post = _mapper.Map<Post>(addPostRequestModel);
        var postWithCategoriesAndTags = post with 
        {
            Categories = await GetCategories(addPostRequestModel.CategoryIds),
            Tags = await GetTags(addPostRequestModel.TagIds)
        };
        
        await _repository.AddOneAsync(postWithCategoriesAndTags);
        return _mapper.Map<AddPostResponseModel>(postWithCategoriesAndTags);
    }

    public async Task<DeletePostResponseModel> DeleteAsync(DeletePostRequestModel deletePostRequestModel)
    {   
        bool result;
        if(deletePostRequestModel.DeleteCompletely)
        {
            var post = await _repository.GetAsync(deletePostRequestModel.PostId);
            result = await _repository.UpdateOneAsync(post with {IsActive = false});
        }
        else
        {
            result = await _repository.DeleteAsync(deletePostRequestModel.PostId);
        }
        
        return new DeletePostResponseModel() { Succeed = result }; 
    }

    public async Task<IEnumerable<FindPostResponseModel>> FindAsync(Expression<Func<Post, bool>> filter)
    {
        var posts = ( await _repository.FindAsync(filter)).Where(p => p.IsActive);
        return _mapper.Map<IEnumerable<FindPostResponseModel>>(posts);
    }

    public async Task<FindPostResponseModel> GetAsync(GetPostByIdRequestModel getPostByIdRequestModel)
    {
        var post = await _repository.GetAsync(getPostByIdRequestModel.Id);
        return _mapper.Map<FindPostResponseModel>(post);
    }

    public async Task<UpdatePostResponseModel> UpdateAsync(UpdatePostRequestModel updatePostRequestModel)
    {
        var post = await _repository.GetAsync(updatePostRequestModel.Id);
        if (post is null) return new UpdatePostResponseModel() { Succeed = false };        
        
        var updatedPost = post with 
        {
            Title = updatePostRequestModel.Title,
            Content = updatePostRequestModel.Content,
            Categories =  await GetCategories(updatePostRequestModel.CategoryIds),
            Tags = await GetTags(updatePostRequestModel.TagIds)
        };

        return new UpdatePostResponseModel() { Succeed = await _repository.UpdateOneAsync(updatedPost) };
    }

    private async Task<List<Category>> GetCategories(List<Guid> categoryIds)
    {
        List<Category> categories = new ();
        
        foreach (var categoryId in categoryIds)
        {
            var category = await _categoryRepository.GetAsync(categoryId);
            categories.Add(category);
        }
        
        return categories;
    }

        private async Task<List<Tag>> GetTags(List<Guid>? tagIds)
    {
        List<Tag> tags = new ();
        foreach (var tagId in tagIds)
        {
            var tag = await _tagRepository.GetAsync(tagId);
            tags.Add(tag);
        }
        return tags;
    }
}