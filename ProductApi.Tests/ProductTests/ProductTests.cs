using Application.DTOs.ProductDTO;
using Application.Services;
using AutoMapper;
using Domain.Entitites;
using Domain.Ports;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Tests.ProductTests
{
    public class ProductTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductService _service;

        public ProductTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ProductService(_mockRepo.Object, _mockCategoryRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrow_WhenCategoryNotFound()
        {
            var dto = new CreateProductDTO { Name = "Prod1", Price = 10, CategoryId = Guid.NewGuid() };
            _mockCategoryRepo.Setup(r => r.GetByIdAsync(dto.CategoryId)).ReturnsAsync((Category?)null);

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task CreateAsync_ShouldAddProduct_WhenCategoryExists()
        {
            var category = new Category { Id = Guid.NewGuid(), Name = "Cat1" };
            var dto = new CreateProductDTO { Name = "Prod1", Price = 10, CategoryId = category.Id };
            var product = new Product { Id = Guid.NewGuid(), Name = dto.Name, Category = category };
            var productDto = new ReadProductDTO { Id = product.Id, Name = product.Name };

            _mockCategoryRepo.Setup(r => r.GetByIdAsync(dto.CategoryId)).ReturnsAsync(category);
            _mockMapper.Setup(m => m.Map<Product>(dto)).Returns(product);
            _mockMapper.Setup(m => m.Map<ReadProductDTO>(product)).Returns(productDto);

            var result = await _service.CreateAsync(dto);

            _mockRepo.Verify(r => r.AddAsync(product), Times.Once);
            Assert.Equal("Prod1", result.Name);
        }

    }
}
