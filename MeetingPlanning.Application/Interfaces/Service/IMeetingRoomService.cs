using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Domain.Entities;
using System.Security.Claims;

namespace MeetingPlanning.Application.Interfaces.Service
{
    public interface IMeetingRoomService
    {
        public Task<List<ListMeetingRoomWithTeamDto>> GetAll();
        public Task<MeetingRoom> GetMeetingRoomById(Guid id);
        public Task<MeetingRoom> CreateMeetingRoom(MeetingRoomDto roomDto);
        public Task<bool> UpdateMeetingRoom(Guid id, MeetingRoomDto roomDto);
        public Task<List<ListMeetingRoomWithTeamDto>> GetMeetingRoomByTeam(ClaimsPrincipal userClaims);


    }
}
