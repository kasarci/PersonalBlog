using System.Linq.Expressions;
using AutoMapper;
using MongoDB.Driver;
using PersonalBlog.Business.Models.Tag;
using PersonalBlog.Business.Models.Tag.Add;
using PersonalBlog.Business.Models.Tag.Delete;
using PersonalBlog.Business.Models.Tag.Find;
using PersonalBlog.Business.Models.Tag.Update;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using Tag = PersonalBlog.DataAccess.Entities.Concrete.Tag;

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

    public async Task<IEnumerable<TagModel>> FindAsync(Expression<Func<DataAccess.Entities.Concrete.Tag, bool>> filter)
    {
        var result = await _repository.FindAsync(filter);
        return _mapper.Map<IEnumerable<TagModel>>(result);
    }

    public async Task<TagModel> FindAsync(FindTagByIdRequestModel findTagByIdRequestModel)
    {
        var result = await _repository.GetAsync(findTagByIdRequestModel.Id);
        return _mapper.Map<TagModel>(result);
    }

    public async Task<TagModel> FindAsync(FindTagByNameRequestModel findTagByNameRequest)
    {
        var result = (await _repository.FindAsync(t => t.Name == findTagByNameRequest.Name)).FirstOrDefault();
        return _mapper.Map<TagModel>(result);
    }

    public async Task<AddTagResponseModel> AddAsync(AddTagRequestModel addTagRequestModel)
    {
        var tag = _mapper.Map<Tag>(addTagRequestModel);
        await _repository.AddOneAsync(tag);
        return _mapper.Map<AddTagResponseModel>(tag);
    }

    public async Task<DeleteTagResponseModel> DeleteAsync(DeleteTagRequestModel deleteTagRequestModel)
    {
        return new DeleteTagResponseModel()
        {
            Succeed = await _repository.DeleteAsync(deleteTagRequestModel.Id)
        };
    }


    public async Task<UpdateTagResponseModel> UpdateAsync(UpdateTagRequestModel updateTagRequestModel)
    {
        var tag = await _repository.GetAsync(updateTagRequestModel.Id);
        if (tag is null)
        {
            return new UpdateTagResponseModel() { Succeed = false };
        }
        
        var updatedTag = tag with {Name = updateTagRequestModel.Name };
        
        return new UpdateTagResponseModel()
        {
            Succeed = await _repository.UpdateOneAsync(updatedTag)
        };
    }
}