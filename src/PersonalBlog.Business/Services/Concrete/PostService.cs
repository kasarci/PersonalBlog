using System.Diagnostics.Tracing;
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
using PersonalBlog.Business.Models.Comment;

namespace PersonalBlog.Business.Services.Concrete;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ICommentRepository _commentRepository;


    public PostService(IPostRepository repository, IMapper mapper, ICategoryRepository categoryRepository, ITagRepository tagRepository, ICommentRepository commentRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _commentRepository = commentRepository;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync(p => p.IsActive);
    public async Task<long> CountByCategoryNameAsync(string categoryName) => await _repository.CountAsync(p => p.IsActive && p.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower()));

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

    public async Task<Guid> AddCommentReference(AddCommentReferenceRequestModel addCommentModel)
    {
        var post = await _repository.GetAsync(addCommentModel.PostId);
        if (post is null) return Guid.Empty;
        
        var comment = await _commentRepository.GetAsync(addCommentModel.CommentId);
        if (comment is null) return Guid.Empty;

        var updatedComments = post.Comments is null ? 
                                new List<Comment>() : 
                                post.Comments?.ToList<Comment>();

        updatedComments.Add(comment);

        var updatedPost = post with 
        {
            Comments = updatedComments
        };

        var succeed = await _repository.UpdateOneAsync(updatedPost);
        
        if (succeed) return comment.Id;
        else return Guid.Empty;
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

    public async Task<bool> DeleteCommentReference(CommentModel? comment)
    {
        var post = await _repository.GetAsync(comment.PostId);
        if (post is null) return false;

        var comments = new List<Comment>();
        comments.AddRange(post.Comments.ToArray());

        comments.Remove(comments.FirstOrDefault(c => c.Id == comment.Id));

        var result = await _repository.UpdateOneAsync( post with {Comments = comments} );

        return result;
    }

    public async Task<IEnumerable<FindPostResponseModel>> FindAsync(Expression<Func<Post, bool>> filter,int number, bool isDescending)
    {
        var posts = ( await _repository.FindAsync(filter,number, isDescending)).Where(p => p.IsActive);
        return _mapper.Map<IEnumerable<FindPostResponseModel>>(posts);
    }

    public async Task<IEnumerable<FindPostResponseModel>> FindAsyncWithPagination(Expression<Func<Post, bool>> filter, int pageIndex, int size)
    {
        var posts = (await _repository.FindAsyncWithPagination(filter, pageIndex, size)).Where(p => p.IsActive);
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