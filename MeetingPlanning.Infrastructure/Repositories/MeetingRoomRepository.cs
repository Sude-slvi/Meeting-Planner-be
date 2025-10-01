using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Repository;
using MeetingPlanning.Domain.Entities;
using MeetingPlanning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeetingPlanning.Infrastructure.Repositories
{
    public class MeetingRoomRepository: IMeetingRoomRepository
    {
        private readonly AppDbContext _context;

        public MeetingRoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MeetingRoom> CreateMeetingRoom(MeetingRoom room)
        {
            _context.MeetingRooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<List<MeetingRoom>> GetAll()
        {
            return await _context.MeetingRooms
                                 .Include(r => r.Teams)
                                 .Where(r => !r.IsDeleted)
                                 .OrderBy(r=>r.Name)
                                 .ToListAsync();

        }

        public async Task<MeetingRoom> GetMeetingRoomById(Guid id)
        {
            var room = await _context.MeetingRooms.FindAsync(id);
            if (room is null) throw new KeyNotFoundException($"Room {id} not found.");
            return room;
        }

        public async Task<bool> UpdateMeetingRoom(MeetingRoom room)
        {
            var existingRoom = await _context.MeetingRooms.FindAsync(room.Id);
            if (existingRoom == null) return false;
            existingRoom.Name = room.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MeetingRoom>> GetMeetingRoomByTeam(int teamId)
        {
            var rooms = await _context.MeetingRooms
                .Include(r => r.Teams) // Teams yükleniyor
                .Where(r => r.Teams.Any(t => t.Id == teamId))
                .ToListAsync();

            return rooms;
        }

    }
}
