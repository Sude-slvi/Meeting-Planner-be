using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Repository;
using MeetingPlanning.Application.Interfaces.Service;
using MeetingPlanning.Domain.Entities;
using System.Security.Claims;

namespace MeetingPlanning.Application.Services
{
    public class MeetingRoomService:IMeetingRoomService    {

        private readonly IMeetingRoomRepository _meetingRoomRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        public MeetingRoomService(IMeetingRoomRepository meetingRoomRepository, IOrganizationRepository organizationRepository, IUserRepository userRepository)
        {
            _meetingRoomRepository = meetingRoomRepository;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ListMeetingRoomWithTeamDto>> GetAll()
        {
            var rooms = await _meetingRoomRepository.GetAll();

            return rooms.Select(r => new ListMeetingRoomWithTeamDto
            {
                Id=r.Id,
                Name = r.Name,
                TeamNames = r.Teams.Select(t => t.Name).ToList()
            }).ToList();
        }

        public async Task<List<ListMeetingRoomWithTeamDto>> GetMeetingRoomByTeam(ClaimsPrincipal userClaims)
        {
            var username = userClaims.FindFirst("preferred_username")?.Value
                      ?? throw new Exception("Token'da username yok.");

            var user = await _userRepository.GetByUsernameAsync(username);

            if (user==null)
                throw new Exception("Kullanıcı bulunamadı.");

            var teamId = user.TeamId;
            var rooms = await _meetingRoomRepository.GetMeetingRoomByTeam(teamId);
            var result = rooms.Select(r => new ListMeetingRoomWithTeamDto
            {
                Id=r.Id,
                Name = r.Name,
                TeamNames= r.Teams.Select(t=>t.Name).ToList()
            }).ToList();

            return result;
        }
        public Task<MeetingRoom> GetMeetingRoomById(Guid id) => _meetingRoomRepository.GetMeetingRoomById(id);
        
        public async Task<MeetingRoom> CreateMeetingRoom(MeetingRoomDto roomDto)
        {
            var teams = new List<Team>();

            foreach (var teamId in roomDto.TeamIds)
            {
                var team = await _organizationRepository.GetTeamById(teamId);
                if (team != null)
                {
                    teams.Add(team);
                }
            }

            var room = new MeetingRoom
            {
                Name = roomDto.Name,
                Teams = teams
            };

            return await _meetingRoomRepository.CreateMeetingRoom(room);
        }

        public Task<bool> UpdateMeetingRoom(Guid id, MeetingRoomDto roomDto)
        {
            return null;
        }


    }
}
