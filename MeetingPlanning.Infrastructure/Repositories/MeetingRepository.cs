using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Repository;
using MeetingPlanning.Domain.Entities;
using MeetingPlanning.Domain.Enums;
using MeetingPlanning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MeetingPlanning.Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly AppDbContext _context;

        public MeetingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasConflictAsync(Guid meetingRoomId, DateTime startTime, DateTime endTime)
        {
            return await _context.Meetings.AnyAsync(m =>
                m.MeetingRoomId == meetingRoomId &&
                m.StartTime < endTime &&
                m.EndTime > startTime);

        }
        //Güncelleme işleminde güncellenen toplantıyı yoksaymak için 
        public async Task<bool> HasConflictAsync(Guid meetingRoomId, DateTime startTime, DateTime endTime, Guid? ignoreId = null)
        {
            return await _context.Meetings.AnyAsync(m =>
                m.MeetingRoomId == meetingRoomId &&
                m.StartTime < endTime &&
                m.EndTime > startTime &&
                (ignoreId == null || m.Id != ignoreId));
        }

        public async Task<List<Meeting>> GetAllMeetings()
        {
            return await _context.Meetings
                                 .Include(r => r.MeetingRoom)
                                 .Include(r => r.User)
                                 .Include(r => r.MeetingInvitations)
                                    .ThenInclude(mi => mi.User)
                                        .ThenInclude(u => u.Team)
                                 .OrderBy(r => r.StartTime)
                                 .ToListAsync();
        }
        //Aşağıdaki ikisi daha iyi yazılabilir miydi generic falan araştır.
        public async Task<List<Meeting>> GetUsersMeetings(string username)
        {

            var user = await _context.Users
                .Include(u => u.Team)
                .FirstOrDefaultAsync(u => u.FullName == username);

            if (user == null || user.TeamId == 0)
                return new List<Meeting>();

            var meetings = await _context.Meetings
            .Include(m => m.MeetingRoom)
            .Include(m => m.User)
            .Include(m => m.MeetingInvitations)
                .ThenInclude(mi => mi.User)
                    .ThenInclude(u => u.Team)
            .Where(m =>
                m.UserId == user.Id||
                m.MeetingInvitations.Any(mi => mi.UserId == user.Id && mi.Status == InvitationStatus.Accepted)
            )
            .OrderBy(m => m.StartTime)
            .ToListAsync();

            return meetings;
        }

        public async Task<List<Meeting>> GetMeetingsByUserTeamAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.Team)
                .FirstOrDefaultAsync(u => u.FullName == username);

            if (user == null || user.TeamId == 0)
                return new List<Meeting>();

            var meetings = await _context.Meetings
                .Include(m => m.MeetingRoom)
                .Include(m => m.User)
                .Include(m => m.MeetingInvitations)
                    .ThenInclude(mi => mi.User)
                        .ThenInclude(u => u.Team)
                .Where(m =>
                    m.User.TeamId == user.TeamId // ekibin toplantıları
                    || m.MeetingInvitations.Any(mi => mi.User.TeamId == user.TeamId && mi.Status == InvitationStatus.Accepted) // kendisinin davetli olduğu kabul edilmiş toplantılar
                )
                .OrderBy(m => m.StartTime)
                .ToListAsync();

            foreach (var meeting in meetings)
            {
                if (meeting.User.TeamId != user.TeamId)
                {
                    meeting.MeetingInvitations = meeting.MeetingInvitations
                        .Where(mi => mi.User.TeamId == user.TeamId && mi.Status == InvitationStatus.Accepted)
                        .ToList();
                }
            }

            return meetings;
            //Ekibinde olan kişilerin davet edildiği ve kabul edilen toplantıların getirilmesini istiyorum.
            //Burada sadece davet edilenler getirilmiş reddeden de listeleniyor. 
            //Ben kabul edilenlerin getirilmesini istiyorum.
        }

        public async Task AddAsync(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Meeting meeting, IEnumerable<Guid> invitedUserIds)
        {
            var trackedMeeting = await _context.Meetings
                .Include(m => m.MeetingInvitations)
                .FirstOrDefaultAsync(m => m.Id == meeting.Id);

            if (trackedMeeting == null)
                throw new KeyNotFoundException("Meeting not found");

            var toRemove = trackedMeeting.MeetingInvitations
                .Where(mi => !invitedUserIds.Contains(mi.UserId))
                .ToList();

            foreach (var remove in toRemove)
                _context.MeetingInvitations.Remove(remove);

            var existingIds = trackedMeeting.MeetingInvitations.Select(mi => mi.UserId).ToHashSet();
            var toAdd = invitedUserIds
                .Where(id => !existingIds.Contains(id))
                .Select(id => new MeetingInvitation
                {
                    MeetingId = trackedMeeting.Id,
                    UserId = id,
                    Status = InvitationStatus.Pending
                })
                .ToList();

            foreach (var add in toAdd)
            {
                _context.Entry(add).State = EntityState.Added;
                trackedMeeting.MeetingInvitations.Add(add);
            }

            trackedMeeting.Title = meeting.Title;
            trackedMeeting.StartTime = meeting.StartTime;
            trackedMeeting.Duration = meeting.Duration;
            trackedMeeting.EndTime = meeting.EndTime;
            trackedMeeting.MeetingRoomId = meeting.MeetingRoomId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.GetType()} - {ex.Message}");
                throw;
            }
        }

        public async Task<Meeting?> GetByIdWithInvitationsAsync(Guid id)
        {
            return await _context.Meetings
                .Include(m => m.MeetingInvitations)
                    .ThenInclude(mi => mi.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Meeting?> GetByIdAsync(Guid meetingId)
        {
            return await _context.Meetings
                .Include(m => m.MeetingInvitations)   
                    .ThenInclude(mi => mi.User)       
                .Include(m => m.MeetingRoom)          
                .Include(m => m.User)                 
                .FirstOrDefaultAsync(m => m.Id == meetingId);
        }

        public async Task<List<MeetingInvitation>> GetAllInvitation(Guid userId)
        {
            return await _context.MeetingInvitations
                .Include(m => m.Meeting)
                    .ThenInclude(r => r.MeetingRoom)
                .Where(m => m.Status==0 && m.User.Id==userId)
                .ToListAsync();
        }
        public async Task<MeetingInvitation?> GetInvitationById(Guid invId)
        {
            return await _context.MeetingInvitations
                .FindAsync(invId);
        }
        public async Task UpdateInvitationStatusAsync(MeetingInvitation invitation)
        {
            _context.MeetingInvitations.Update(invitation);
            await _context.SaveChangesAsync();
        }
    }
}
