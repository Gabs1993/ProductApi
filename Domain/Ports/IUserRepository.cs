using Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IUserRepository
    {
        Task<Users?> GetByEmailAsync(string email);
        Task AddAsync(Users user);
        Task<Users?> GetByIdAsync(Guid id);
        Task<IEnumerable<Users>> GetAllAsync();
        Task UpdateAsync(Users user);
        Task DeleteAsync(Users user);
    }
}
