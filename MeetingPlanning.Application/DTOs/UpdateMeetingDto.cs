using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.DTOs
{
    public class UpdateMeetingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid MeetingRoomId { get; set; }
        public List<Guid> InvitedUserIds { get; set; } = new();
    }
}
