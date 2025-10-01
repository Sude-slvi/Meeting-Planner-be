using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Domain.Entities;

namespace MeetingPlanning.Application.Interfaces.Repository
{
    public interface IMeetingRoomRepository
    {
        Task<List<MeetingRoom>> GetAll();
        Task<MeetingRoom> GetMeetingRoomById(Guid id);
        Task<MeetingRoom> CreateMeetingRoom(MeetingRoom room);
        Task<bool> UpdateMeetingRoom(MeetingRoom room);
        Task<List<MeetingRoom>> GetMeetingRoomByTeam(int teamId);

    }
}
