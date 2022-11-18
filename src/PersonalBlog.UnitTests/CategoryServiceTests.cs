using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using PersonalBlog.Business.Mapping;
using PersonalBlog.Business.Models.Category;
using PersonalBlog.Business.Models.Category.Add;
using PersonalBlog.Business.Models.Category.Delete;
using PersonalBlog.Business.Models.Category.Find;
using PersonalBlog.Business.Models.Category.Update;
using PersonalBlog.Business.Services.Concrete;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using Xunit;

namespace PersonalBlog.UnitTests;

public class CategoryServiceTests
{
    private readonly IMapper _mapperStub;
    private readonly Mock<ICategoryRepository> _repositoryStub;

    public CategoryServiceTests()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CategoryProfile()));
        _mapperStub = new Mapper(configuration);
        _repositoryStub = new Mock<ICategoryRepository>();
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithUnexistingCategory_ReturnsNull()
    {
        //Arrange
        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                        .ReturnsAsync((Category) null);

        var service = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await service.GetAsync(new GetCategoryByIdRequestModel{Id = Guid.NewGuid()});

        //Assert
        Assert.Null(result);        
    }

    [Fact]
    public async Task GetCategoryByNameAsync_WithUnexistingCategory_ReturnsNull()
    {
        //Arrange
        _repositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Category,bool>>>())).
            ReturnsAsync(new List<Category>());

        var service = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await service.GetAsync(new GetCategoryByNameRequestModel{Name = Guid.NewGuid().ToString()});

        //Assert
        Assert.Null(result);        
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithExistingCategory_ReturnsExpectedItem()
    {
        //Arrange
        Category expectedCategory = CreateRandomCategory();

        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(expectedCategory);

        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.GetAsync(new GetCategoryByIdRequestModel{Id = Guid.NewGuid()});
        
        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<CategoryModel>(expectedCategory));
    }

    [Fact]
    public async Task GetCategoryByNameAsync_WithExistingCategory_ReturnsExpectedItem()
    {
        //Arrange
        Category expectedCategory = CreateRandomCategory();

        _repositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Category,bool>>>())).
            ReturnsAsync(new List<Category>{expectedCategory});

        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.GetAsync(new GetCategoryByNameRequestModel{Name = Guid.NewGuid().ToString()});
        
        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<CategoryModel>(expectedCategory));
    }

    [Fact]
    public async Task FindCategoriesAsync_WithUnexistingCategories_ReturnsEmptyList()
    {
        //Arrange

        _repositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Category, bool>>>())).
            ReturnsAsync(new List<Category>());

        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.FindAsync(x => x.Id != null);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task FindCategoriesAsync_WithExistingCategories_ReturnsAllCategories()
    {
        //Arrange
        var expectedCategories = CreateRandomCategories(3);

        _repositoryStub.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Category, bool>>>())).
            ReturnsAsync(expectedCategories);

        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.FindAsync(x => x.Id != null);

        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<List<CategoryModel>>(expectedCategories));
    }

    [Fact]
    public async Task CreateCategory_WithValidCategory_ReturnsCategoryAsAddCategoryResponseModel()
    {
        //Arrange
        var expectedCategory = CreateRandomCategory();

        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.AddAsync(new AddCategoryRequestModel{Name = expectedCategory.Name});

        //Assert
        result.Should().BeEquivalentTo(_mapperStub.Map<AddCategoryResponseModel>(expectedCategory), opt => opt.ComparingByMembers<AddCategoryResponseModel>().Excluding(c => c.Id))
        .And.BeOfType<AddCategoryResponseModel>();

        result.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task UpdateCategory_WithUnexistingCategory_ReturnFalseSucceedResponse()
    {
        //Arrange
        var expectedResult = new UpdateCategoryResponseModel{ Succeed = false };

        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Category) null);
        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.UpdateAsync(new UpdateCategoryRequestModel{Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString()});

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task UpdateCategory_WithValidCategory_ReturnTrueSucceedResponse()
    {
        //Arrange
        var expectedResult = new UpdateCategoryResponseModel{ Succeed = true };

        var expectedCategory = CreateRandomCategory();

        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(expectedCategory);
        _repositoryStub.Setup(repo => repo.UpdateOneAsync(It.IsAny<Category>())).ReturnsAsync(true);
        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.UpdateAsync(new UpdateCategoryRequestModel{Id = expectedCategory.Id, Name = Guid.NewGuid().ToString()});

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task DeleteCategory_WithInvalidCategory_ReturnFalseSucceedResponse()
    {
        //Arrange
        var expectedResult = new DeleteCategoryResponseModel{ Succeed = false };

        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Category) null);
        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.DeleteAsync(new DeleteCategoryRequestModel{Id = Guid.NewGuid()});

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task DeleteCategory_WithValidCategory_ReturnTrueSucceedResponse()
    {
        //Arrange
        var expectedResult = new DeleteCategoryResponseModel{ Succeed = true };

        var expectedCategory = CreateRandomCategory();

        _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(expectedCategory);
        _repositoryStub.Setup(repo => repo.DeleteAsync(It.IsAny<Category>())).ReturnsAsync(true);
        var categoryService = new CategoryService(_repositoryStub.Object, _mapperStub);

        //Act
        var result = await categoryService.DeleteAsync(new DeleteCategoryRequestModel{Id = expectedCategory.Id});

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    private Category CreateRandomCategory()
    {
        return new Category()
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }

    private List<Category> CreateRandomCategories(int count)
    {
        if (count <= 0) count = 1;

        var categories = new List<Category>();
        for (int i = 0; i < count; i++)
        {
            categories.Add(CreateRandomCategory());
        }
        return categories;
    }
}