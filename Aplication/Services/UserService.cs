using Application.DTOs.UserDTO;
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
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadUserDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadUserDTO>>(users);
        }

        public async Task<ReadUserDTO?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<ReadUserDTO?> GetByEmailAsync(string email)
        {
            var user = await _repository.GetByEmailAsync(email);
            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<ReadUserDTO> CreateAsync(CreateUserDTO dto)
        {
            var user = _mapper.Map<Users>(dto);
            await _repository.AddAsync(user);
            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task UpdateAsync(UpdateUserDTO dto)
        {
            var user = _mapper.Map<Users>(dto);
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user != null)
                await _repository.DeleteAsync(user);
        }
    }
}
