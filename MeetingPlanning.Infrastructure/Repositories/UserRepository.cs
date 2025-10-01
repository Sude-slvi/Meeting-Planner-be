using MeetingPlanning.Application.Interfaces.Repository;
using MeetingPlanning.Domain.Entities;


using MeetingPlanning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
            .Include(u => u.Team)
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Team)
                .FirstOrDefaultAsync(u => u.FullName == username);
        }

        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<List<User>> GetAllUsersExceptUser(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Team)
                .Where(u => u.Id != userId)
                .ToListAsync();
        }


    }
}
