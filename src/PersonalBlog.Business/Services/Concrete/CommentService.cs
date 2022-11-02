using AutoMapper;
using PersonalBlog.Business.Models.Comment.Add;
using PersonalBlog.Business.Models.Comment.Delete;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.Business.Services.Concrete;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<AddCommentResponseModel> AddAsync(AddCommentRequestModel addCommentRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(DeleteCommentRequestModel deleteCommentRequestModel)
    {
        throw new NotImplementedException();
    }

    Task<DeleteCommentResponseModel> ICommentService.DeleteAsync(DeleteCommentRequestModel deleteCommentRequestModel)
    {
        throw new NotImplementedException();
    }
}