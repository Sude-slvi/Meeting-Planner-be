using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Domain.Entities;

namespace MeetingPlanning.Application.Interfaces.Repository
{
    public interface IMeetingRepository
    {
        Task<bool> HasConflictAsync(Guid meetingRoomId, DateTime startTime, DateTime endTime);
        Task<List<Meeting>> GetAllMeetings();

        Task AddAsync(Meeting meeting);
        Task SaveChangesAsync();
        Task UpdateAsync(Meeting meeting, IEnumerable<Guid> invitedUserIds);
        Task<bool> HasConflictAsync(Guid meetingRoomId, DateTime startTime, DateTime endTime, Guid? ignoreId = null);
        Task<List<Meeting>> GetMeetingsByUserTeamAsync(string username);
        Task<Meeting?> GetByIdWithInvitationsAsync(Guid id);
        Task<Meeting?> GetByIdAsync(Guid meetingId);
        Task<List<MeetingInvitation>> GetAllInvitation(Guid userId);
        Task<MeetingInvitation?> GetInvitationById(Guid invId);
        Task<List<Meeting>> GetUsersMeetings(string username);
        Task UpdateInvitationStatusAsync(MeetingInvitation invitation);

    }
}
