using AutoMapper;
using PersonalBlog.Business.Models.Tag;
using PersonalBlog.Business.Models.Tag.Add;
using PersonalBlog.Business.Models.Tag.Delete;
using PersonalBlog.Business.Models.Tag.Find;
using PersonalBlog.Business.Models.Tag.Update;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.Business.Services.Concrete;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;
    private readonly IMapper _mapper;

    public TagService(ITagRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<AddTagResponseModel> AddAsync(AddTagRequestModel addTagRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(DeleteTagRequestModel deleteTagRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task<TagModel> FindAsync(FindTagByIdRequestModel findTagByIdRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task<TagModel> FindAsync(FindTagByNameRequestModel findTagByNameRequest)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdateTagRequestModel updateTagRequestModel)
    {
        throw new NotImplementedException();
    }
}