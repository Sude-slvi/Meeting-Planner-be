using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Domain.Entities
{
    public class Meeting
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = default!; 
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime { get; set; }
        public Guid MeetingRoomId { get; set; }
        public MeetingRoom MeetingRoom { get; set; } = default!;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public ICollection<MeetingInvitation> MeetingInvitations { get; set; } = new List<MeetingInvitation>();

    }
}
