using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.Interfaces.Service
{
    public interface IUserService
    {
        Task<User> GetOrCreateUserAsync(ClaimsPrincipal userClaims);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task<List<ListUsersDto>> GetAllUsersExceptUser(Guid userId);

    }
}
