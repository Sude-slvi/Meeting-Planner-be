using AutoMapper;
using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Repository;
using MeetingPlanning.Application.Interfaces.Service;
using MeetingPlanning.Application.Security.Claims;
using MeetingPlanning.Domain.Entities;
using MeetingPlanning.Domain.Enums;


namespace MeetingPlanning.Application.Services
{
    public class MeetingService : IMeetingService
    {

        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MeetingService(IMeetingRepository meetingRepository, IMapper mapper, IUserRepository userRepository)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
            _userRepository=userRepository;
        }

        public async Task<Guid> CreateMeetingAsync(
            string title,
            DateTime startTime,
            TimeSpan duration,
            Guid meetingRoomId,
            Guid userId,
            IEnumerable<Guid> invitedUserIds)
        {
            var startUtc = startTime.ToUniversalTime();
            var newEndTime = startUtc.Add(duration);

            var hasConflict = await _meetingRepository.HasConflictAsync(meetingRoomId, startUtc, newEndTime);
            if (hasConflict)
            {
                throw new InvalidOperationException("Bu saat aralığı doludur.");
            }

            var meeting = new Meeting
            {
                Title = title,
                StartTime = startUtc,
                Duration = duration,
                MeetingRoomId = meetingRoomId,
                UserId = userId,
                EndTime = newEndTime
            };

            if (invitedUserIds != null)
            {
                foreach (var invitedUserId in invitedUserIds)
                {
                    meeting.MeetingInvitations.Add(new MeetingInvitation
                    {
                        Meeting = meeting,
                        UserId = invitedUserId,
                        Status = InvitationStatus.Pending
                    });
                }
            }

            await _meetingRepository.AddAsync(meeting);

            try
            {
                await _meetingRepository.SaveChangesAsync();
            }
            catch (Exception ex) when (ex.InnerException?.Message.Contains("no_overlapping_meetings") == true)
            {
                throw new InvalidOperationException("Bu saat aralığı doludur.", ex);
            }

            return meeting.Id;
        }

        public async Task<List<ListMeetingDto>> GetAllMeetings()
        {
            var meetings = await _meetingRepository.GetAllMeetings();

            try
            {
                var responseDto = _mapper.Map<List<ListMeetingDto>>(meetings);
                return responseDto;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        public async Task<List<ListMeetingDto>> GetUsersMeetingsByUsername(string username)
        {
            var meetings = await _meetingRepository.GetUsersMeetings(username);

            var response = _mapper.Map<List<ListMeetingDto>>(meetings);

            return response;
        }

        public async Task<List<ListMeetingDto>> GetTeamMeetingsByUsername(string username)
        {
            var meetings = await _meetingRepository.GetMeetingsByUserTeamAsync(username);

            var response = _mapper.Map<List<ListMeetingDto>>(meetings);

            return response;
        }

        public async Task UpdateMeetingAsync(UpdateMeetingDto dto)
        {
            var meeting = await _meetingRepository.GetByIdAsync(dto.Id);
            if (meeting == null)
                throw new KeyNotFoundException("Toplantı bulunamadı.");

            var startUtc = dto.StartTime.ToUniversalTime();
            var endUtc = startUtc.Add(dto.Duration);

            var hasConflict = await _meetingRepository.HasConflictAsync(dto.MeetingRoomId, startUtc, endUtc, dto.Id);
            if (hasConflict)
                throw new InvalidOperationException("Bu saat aralığı doludur.");

            await _meetingRepository.UpdateAsync(new Meeting
            {
                Id = dto.Id,
                Title = dto.Title,
                StartTime = startUtc,
                Duration = dto.Duration,
                EndTime = endUtc,
                MeetingRoomId = dto.MeetingRoomId
            }, dto.InvitedUserIds);
        }
        public async Task<List<ListMeetingInvitationDto>> GetAllInvitation(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if(user==null)
            {
                return null;
            }
            else
            {
                var meetingInvitations = await _meetingRepository.GetAllInvitation(user.Id);
                var dto = _mapper.Map<List<ListMeetingInvitationDto>>(meetingInvitations);
                return dto;
            }
        }

        public async Task UpdateInvitationStatus(UpdateInvitationDto dto)
        {
            var invitation = await _meetingRepository.GetInvitationById(dto.Id);

            if (invitation == null)
                throw new KeyNotFoundException("Davet bulunamadı.");

            invitation.Status = dto.Status;

            await _meetingRepository.UpdateInvitationStatusAsync(invitation);
        }
    }
}
