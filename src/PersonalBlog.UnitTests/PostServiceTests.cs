using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using PersonalBlog.Business.Mapping;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.Business.Models.Post.Update;
using PersonalBlog.Business.Services.Concrete;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using Xunit;

namespace PersonalBlog.UnitTests;

public class PostServiceTests
{
    private readonly Mock<IPostRepository> _postRepositoryStub;
    private readonly Mock<ICategoryRepository> _categoryRepositoryStub;
    private readonly Mock<ITagRepository> _tagRepositoryStub;
    private readonly IMapper _mapperStub;

    public PostServiceTests()
    {
        _postRepositoryStub = new Mock<IPostRepository>();
        _categoryRepositoryStub = new Mock<ICategoryRepository>();
        _tagRepositoryStub = new Mock<ITagRepository>();

        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new PostProfile()));
        _mapperStub = new Mapper(configuration);
    }

    [Fact]
    public async Task GetPostByIdAsync_WithUnexistingPost_ReturnsNull()
    {
        //Arrange
        _postRepositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Post) null);
        
        var postService = new PostService(_postRepositoryStub.Object, _mapperStub, _categoryRepositoryStub.Object, _tagRepositoryStub.Object);

        //Act
        var result = await postService.GetAsync(new GetPostByIdRequestModel { Id = Guid.NewGuid() });

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetPostByIdAsync_WithExistingPost_ReturnsExpectedPost()
    {
        //Arrange
        var expectedPost = CreateRandomPost();
        _postRepositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedPost);
    
        var postService = new PostService(_postRepositoryStub.Object, _mapperStub, _categoryRepositoryStub.Object, _tagRepositoryStub.Object);

        //Act
        var result = await postService.GetAsync(new GetPostByIdRequestModel{ Id = Guid.NewGuid() });

        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<FindPostResponseModel>(expectedPost));
    }

    [Fact]
    public async Task FindPostsAsync_WithUnexistingPosts_ReturnsEmptyList()
    {
        //Arrange
        _postRepositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Post, bool>>>()))
            .ReturnsAsync(new List<Post>());

        var postService = new PostService(_postRepositoryStub.Object, _mapperStub, _categoryRepositoryStub.Object, _tagRepositoryStub.Object);
        
        //Act
        var result = await postService.FindAsync(x => x.IsActive);

        //Assert
        result.Should().BeEmpty().And.BeOfType<List<FindPostResponseModel>>();
    }

    [Fact]
    public async Task FindPostAsync_WithExistingPosts_ReturnsAllPosts()
    {
        //Arrange
        var expectedPosts = CreateRandomPosts(3).ToList();
        _postRepositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Post,bool>>>()))
            .ReturnsAsync(expectedPosts);
        
        var postService = new PostService(_postRepositoryStub.Object, _mapperStub, _categoryRepositoryStub.Object, _tagRepositoryStub.Object);

        //Act
        var result = await postService.FindAsync(p => p.IsActive);
        
        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<List<FindPostResponseModel>>(expectedPosts));
    }

    [Fact]
    public async Task DeletePostAsync_WithValidPost_ReturnsTrueSucceedResult()
    {
        //Arrange
        var expectedResult = new DeletePostResponseModel(){ Succeed = true };
        _postRepositoryStub.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var postService = new PostService(_postRepositoryStub.Object, _mapperStub, _categoryRepositoryStub.Object, _tagRepositoryStub.Object);

        //Act
        var result = await postService.DeleteAsync(new DeletePostRequestModel{ PostId = Guid.NewGuid() });
        
        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task UpdatePostAsync_WithValidPost_ReturnsTrueSucceedResult()
    {
        //Arrange
        var expectedResult = new UpdatePostResponseModel(){ Succeed = true };
        _postRepositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(CreateRandomPost());
        _postRepositoryStub.Setup(repo => repo.UpdateOneAsync(It.IsAny<Post>()))
            .ReturnsAsync(true);

        var postService = new PostService(_postRepositoryStub.Object, _mapperStub, _categoryRepositoryStub.Object, _tagRepositoryStub.Object);

        //Act
        var result = await postService.UpdateAsync(new UpdatePostRequestModel{Id = Guid.NewGuid(), Title = Guid.NewGuid().ToString(), Content = Guid.NewGuid().ToString(), CategoryIds = new List<Guid>(), TagIds = new List<Guid>()});
        
        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    private Post CreateRandomPost()
    {
        return new Post()
        {
            Id = Guid.NewGuid(),
            WriterId = Guid.NewGuid(),
            Title = Guid.NewGuid().ToString(),
            Content = Guid.NewGuid().ToString(),
            CreatedAt = DateTimeOffset.UtcNow,
            Comments = new List<Comment>(),
            IsActive = true,
            Categories = new List<Category>()
        };
    }

    private IEnumerable<Post> CreateRandomPosts(int number)
    {
        for (int i = 0; i < number; i++)
            yield return CreateRandomPost();
    }
}