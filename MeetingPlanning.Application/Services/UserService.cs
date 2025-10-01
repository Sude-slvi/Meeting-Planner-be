using AutoMapper;
using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Repository;
using MeetingPlanning.Application.Interfaces.Service;
using MeetingPlanning.Domain.Entities;
using MeetingPlanning.Domain.Exceptions;
using System.Security.Claims;

namespace MeetingPlanning.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<User> GetOrCreateUserAsync(ClaimsPrincipal userClaims)
        {
            var username = userClaims.FindFirst("preferred_username")?.Value
                      ?? throw new Exception("Token'da username yok.");

            var roleClaims = userClaims.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
            if(roleClaims.Contains("admin", StringComparer.OrdinalIgnoreCase)) 
            {
                return null;
            }

            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser != null) return existingUser;

            Team? team = null;
            foreach (var role in roleClaims)
            {
                team = await _organizationRepository.GetTeamByNameAsync(role);
                if (team != null) break;
            }

            if (team == null)
                throw new TeamExceptions("Kullanıcının rolü veritabanındaki herhangi bir Team ile eşleşmiyor.");


            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = username,
                TeamId = team.Id
            };

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return newUser;
        }
        public  Task<User?> GetByUsernameAsync(string username)
        {
            return _userRepository.GetByUsernameAsync(username);
        }

        public Task<User?> GetUserByIdAsync(Guid id)
        {
            return _userRepository.GetUserByIdAsync(id);
        }
        public async Task<List<ListUsersDto>> GetAllUsersExceptUser(Guid userId)
        {
            var users = await _userRepository.GetAllUsersExceptUser(userId);

            var dto = _mapper.Map<List<ListUsersDto>>(users);

            return dto;
        }

    }

}
