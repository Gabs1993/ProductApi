using Application.DTOs.ProductDTO;
using AutoMapper;
using Domain.Entitites;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadProductDTO>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadProductDTO>>(products);
        }

        public async Task<ReadProductDTO?> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReadProductDTO>(product);
        }

        public async Task<ReadProductDTO> CreateAsync(CreateProductDTO dto)
        {
            
            var category = await _repository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Invalid category. Register a category before creating a product.");
            }

            var product = _mapper.Map<Product>(dto);
            await _repository.AddAsync(product);
            return _mapper.Map<ReadProductDTO>(product);
        }

        public async Task UpdateAsync(UpdateProductDTO dto)
        {
            
            var category = await _repository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Categoria inválida.");
            }

            var product = _mapper.Map<Product>(dto);
            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product != null)
                await _repository.DeleteAsync(product);
        }
    }
}
