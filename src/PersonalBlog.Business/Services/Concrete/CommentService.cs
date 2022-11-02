using System.Linq.Expressions;
using AutoMapper;
using PersonalBlog.Business.Models.Comment;
using PersonalBlog.Business.Models.Comment.Add;
using PersonalBlog.Business.Models.Comment.Delete;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Entities.Concrete;
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
    
    public async Task<IEnumerable<CommentModel>> FindAsync(Expression<Func<Comment, bool>> filter)
    {
        var comments = await _repository.FindAsync(filter);
        return _mapper.Map<IEnumerable<CommentModel>>(comments);
    }

    public async Task<AddCommentResponseModel> AddAsync(AddCommentRequestModel addCommentRequestModel)
    {
        var comment = _mapper.Map<Comment>(addCommentRequestModel);
        await _repository.AddOneAsync(comment);
        return _mapper.Map<AddCommentResponseModel>(comment);
    }

    public async Task<DeleteCommentResponseModel> DeleteAsync(DeleteCommentRequestModel deleteCommentRequestModel)
    {
        return new DeleteCommentResponseModel() 
        {
            Succeed = await _repository.DeleteAsync(deleteCommentRequestModel.Id)
        };
    }
}