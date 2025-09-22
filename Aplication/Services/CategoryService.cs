using Application.DTOs.CategoryDTO;
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
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadCategoryDTO>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadCategoryDTO>>(categories);
        }

        public async Task<ReadCategoryDTO?> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReadCategoryDTO>(category);
        }

        public async Task<ReadCategoryDTO> CreateAsync(CreateCategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            return _mapper.Map<ReadCategoryDTO>(category);
        }

        public async Task UpdateAsync(UpdateCategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category != null)
                await _repository.DeleteAsync(category);
        }
    }
}
