using MeetingPlanning.Domain.Entities;
using MeetingPlanning.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.DTOs
{
    public class ListMeetingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime { get; set; }
        public Guid MeetingRoomId { get; set; }
        public string MeetingRoomName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public List<ListUsersDto> InvitedUsers { get; set; }

    }
}
