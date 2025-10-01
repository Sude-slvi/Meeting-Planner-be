using MeetingPlanning.Domain.Entities;
using MeetingPlanning.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.DTOs
{
    public class ListMeetingInvitationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MeetingTitle { get; set; } = default!;
        public string UserFullName { get; set; } = default!;
        public InvitationStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime { get; set; }
        public string MeetingRoomName { get; set; } = default!;
    }
}
