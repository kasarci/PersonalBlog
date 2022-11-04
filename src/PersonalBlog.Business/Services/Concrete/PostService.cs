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
        await _repository.AddOneAsync(post);
        return _mapper.Map<AddPostResponseModel>(post);
    }

    public async Task<DeletePostResponseModel> DeleteAsync(DeletePostRequestModel deletePostRequestModel)
    {   
        bool result = false;
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
       
        List<Category> categories = new ();
        List<Tag> tags = new ();
        categories.AddRange((IEnumerable<Category>) updatePostRequestModel.CategoryIds.Select(async categoryId => await _categoryRepository.GetAsync(categoryId)));
        tags.AddRange(collection: (IEnumerable<Tag>) updatePostRequestModel?.TagIds.Select(async tagId => await _tagRepository.GetAsync(tagId)));
        var updatedPost = post with 
        {
            Title = updatePostRequestModel.Title,
            Content = updatePostRequestModel.Content,
            Categories =  categories,
            Tags = tags
        };

        return new UpdatePostResponseModel() { Succeed = await _repository.UpdateOneAsync(updatedPost) };
    }
}