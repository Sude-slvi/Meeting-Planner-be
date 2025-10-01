using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.DTOs
{
    public class ListMeetingRoomWithTeamDto
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public List<string> TeamNames { get; set; } = new();
    }
}
