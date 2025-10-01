using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Domain.Entities;
using System.Security.Claims;

namespace MeetingPlanning.Application.Interfaces.Service
{
    public interface IMeetingService
    {
        public Task<Guid> CreateMeetingAsync(string title, DateTime startTime, TimeSpan duration, Guid meetingRoomId, Guid userId, IEnumerable<Guid> invitedUserIds);
        public Task<List<ListMeetingDto>> GetAllMeetings();
        public Task UpdateMeetingAsync(UpdateMeetingDto dto);
        public Task<List<ListMeetingDto>> GetUsersMeetingsByUsername(string username);
        public Task<List<ListMeetingDto>> GetTeamMeetingsByUsername(string username);
        public Task<List<ListMeetingInvitationDto>> GetAllInvitation(string username);
        public Task UpdateInvitationStatus(UpdateInvitationDto dto);

    }
}
