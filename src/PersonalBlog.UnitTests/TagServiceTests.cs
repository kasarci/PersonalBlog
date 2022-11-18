using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using PersonalBlog.Business.Mapping;
using PersonalBlog.Business.Models.Tag;
using PersonalBlog.Business.Models.Tag.Find;
using PersonalBlog.Business.Services.Concrete;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using Xunit;
using System.Linq;
using PersonalBlog.Business.Models.Tag.Add;
using PersonalBlog.Business.Models.Tag.Delete;
using PersonalBlog.Business.Models.Tag.Update;

namespace PersonalBlog.UnitTests;

public class TagServiceTests
{
    private readonly IMapper _mapperStub;
    private readonly Mock<ITagRepository> _repositoryStub; 

    public TagServiceTests()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new TagProfile()));
        _mapperStub = new Mapper(configuration);
        _repositoryStub = new Mock<ITagRepository> ();
    }

    [Fact]
    public async Task GetTagByIdAsync_WithUnexistingTag_ReturnsNull()
    {
        //Arrange
        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).
            ReturnsAsync((Tag) null);

        var tagService = new TagService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await tagService.FindAsync(new FindTagByIdRequestModel{ Id = Guid.NewGuid() });

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTagByNameAsync_WithUnexistingTag_ReturnsNull()
    {
        //Arrange
        _repositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Tag, bool>>>()))
            .ReturnsAsync(new List<Tag>());

        var tagService = new TagService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await tagService.FindAsync(new FindTagByNameRequestModel{ Name = Guid.NewGuid().ToString() });

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTagByIdAsync_WithExistingTag_ReturnsExpectedTag()
    {
        //Arrange
        Tag expectedTag = CreateRandomTag();

        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedTag);

        var tagService = new TagService(_repositoryStub.Object, _mapperStub );
    
        //Act
        var result = await tagService.FindAsync(new FindTagByIdRequestModel{ Id = expectedTag.Id });

        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<TagModel>(expectedTag));
    }

    [Fact]
    public async Task GetTagByNameAsync_WithExistingTag_ReturnsExpectedTag()
    {
        //Arrange
        Tag expectedTag = CreateRandomTag();

        _repositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Tag,bool>>>()))
            .ReturnsAsync(new List<Tag>{ expectedTag });

        var tagService = new TagService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await tagService.FindAsync(new FindTagByNameRequestModel()
        {
            Name = expectedTag.Name
        });

        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<TagModel>(expectedTag));
    }

    [Fact]
    public async Task FindTags_WithUnexistingTags_ReturnsEmptyListOfTags()
    {
        //Arrange
        _repositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Tag, bool>>>()))
            .ReturnsAsync(new List<Tag>());
        
        var tagService = new TagService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await tagService.FindAsync(x => x.IsActive);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task FindTags_WithExistingTags_ReturnAllTags()
    {
        //Arrange
        var expectedTags = CreateRandomTags(3).ToList();
        _repositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Tag, bool>>>()))
            .ReturnsAsync(expectedTags);

        var tagService = new TagService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await tagService.FindAsync(x => x.IsActive);

        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<List<TagModel>>(result));
    }

    [Fact]
    public async Task CreateTag_WithValidTag_ReturnsTagAsAddTagResponseModel()
    {
        //Arrange
        var expectedTag = CreateRandomTag();

        var tagService = new TagService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await tagService.AddAsync(new AddTagRequestModel()
        {
            Name = expectedTag.Name
        });

        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<AddTagResponseModel>(expectedTag), opt => opt.Excluding(t => t.Id)).And.BeOfType<AddTagResponseModel>();
    }

    [Fact]
    public async Task DeleteTag_WithValidTag_ReturnsTrueSucceedResponse()
    {
        //Arrange
        var expectedResult = new DeleteTagResponseModel() { Succeed = true };
        _repositoryStub.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var tagService = new TagService(_repositoryStub.Object, _mapperStub); 

        //Act
        var result = await tagService.DeleteAsync(new DeleteTagRequestModel(){
            Id = Guid.NewGuid()
        });

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task DeleteTag_WithInvalidTag_ReturnsFalseSucceedResponse()
    {
        //Arrange
        var expectedResult = new DeleteTagResponseModel() { Succeed = false };

        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Tag) null);

        var tagService = new TagService(_repositoryStub.Object, _mapperStub); 

        //Act
        var result = await tagService.DeleteAsync(new DeleteTagRequestModel(){
            Id = Guid.NewGuid()
        });

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task UpdateTag_WithValidTag_ReturnsTrueSucceedResponse()
    {
        //Arrange
        var expectedResult = new UpdateTagResponseModel { Succeed = true };
        var expectedTag = CreateRandomTag();

        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedTag);
        _repositoryStub.Setup(repo => repo.UpdateOneAsync(It.IsAny<Tag>()))
            .ReturnsAsync(true);

        var tagService = new TagService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await tagService.UpdateAsync(new UpdateTagRequestModel {Name = Guid.NewGuid().ToString(), Id = expectedTag.Id });

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task UpdateTag_WithInvalidTag_ReturnsFalseSucceedResponse()
    {
        //Arrange
        var expectedResult = new UpdateTagResponseModel{ Succeed = false };
        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Tag) null);

        var tagService = new TagService(_repositoryStub.Object, _mapperStub);
        
        //Act
        var result = await tagService.UpdateAsync(new UpdateTagRequestModel 
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString()
        });

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    private Tag CreateRandomTag()
    {
        return new Tag()
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            CreatedAt = DateTimeOffset.UtcNow,
            IsActive = true        
        };
    }

    private IEnumerable<Tag> CreateRandomTags(int size)
    {
        for (int i = 0; i < size; i++)
            yield return CreateRandomTag();
    }
}