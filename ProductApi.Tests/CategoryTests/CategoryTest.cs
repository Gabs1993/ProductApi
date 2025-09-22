using Application.DTOs.CategoryDTO;
using Application.Services;
using AutoMapper;
using Domain.Entitites;
using Domain.Ports;
using Infra.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Tests.CategoryTests
{
    public class CategoryTest
    {
        private readonly Mock<ICategoryRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryService _service;

        public CategoryTest()
        {
            _mockRepo = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CategoryService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Cat1", Description = "Desc1" }
            };

            var categoriesDto = new List<ReadCategoryDTO>
            {
                new ReadCategoryDTO { Id = categories[0].Id, Name = "Cat1", Description = "Desc1" }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);
            _mockMapper.Setup(m => m.Map<IEnumerable<ReadCategoryDTO>>(categories)).Returns(categoriesDto);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Cat1", ((List<ReadCategoryDTO>)result)[0].Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnMappedCategory()
        {
            var category = new Category { Id = Guid.NewGuid(), Name = "Cat1", Description = "Desc1" };
            var categoryDto = new ReadCategoryDTO { Id = category.Id, Name = category.Name, Description = category.Description };

            _mockRepo.Setup(r => r.GetByIdAsync(category.Id)).ReturnsAsync(category);
            _mockMapper.Setup(m => m.Map<ReadCategoryDTO>(category)).Returns(categoryDto);

            var result = await _service.GetByIdAsync(category.Id);

            Assert.NotNull(result);
            Assert.Equal("Cat1", result!.Name);
        }

        [Fact]
        public async Task CreateAsync_ShouldMapAndReturnCategory()
        {
            var createDto = new CreateCategoryDTO { Name = "Cat1", Description = "Desc1" };
            var category = new Category { Id = Guid.NewGuid(), Name = "Cat1", Description = "Desc1" };
            var categoryDto = new ReadCategoryDTO { Id = category.Id, Name = "Cat1", Description = "Desc1" };

            _mockMapper.Setup(m => m.Map<Category>(createDto)).Returns(category);
            _mockMapper.Setup(m => m.Map<ReadCategoryDTO>(category)).Returns(categoryDto);

            var result = await _service.CreateAsync(createDto);

            _mockRepo.Verify(r => r.AddAsync(category), Times.Once);
            Assert.Equal("Cat1", result.Name);
        }
    }
}
