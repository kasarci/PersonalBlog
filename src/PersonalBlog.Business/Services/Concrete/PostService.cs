using AutoMapper;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.Business.Models.Post.Update;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.Business.Services.Concrete;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public Task<AddPostResponseModel> AddAsync(AddPostRequestModel addPostRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(DeletePostRequestModel deletePostRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task<FindPostByIdResponseModel> FindAsync(FindPostByIdRequestModel findPostByIdRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdatePostRequestModel updatePostRequestModel)
    {
        throw new NotImplementedException();
    }
}