using Domain.Entitites;
using Domain.Ports;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductContext _context;

        public UserRepository(ProductContext context) => _context = context;

        public async Task<IEnumerable<Users>> GetAllAsync()
            => await _context.Users.ToListAsync();

        public async Task<Users?> GetByIdAsync(Guid id)
            => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<Users?> GetByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Users user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


    }
}
