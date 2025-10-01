using MeetingPlanning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingPlanning.Infrastructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<MeetingInvitation> MeetingInvitations { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }

    }
}
