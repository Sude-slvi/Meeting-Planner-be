using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.DTOs
{
    public class MeetingRoomDto
    {
        public string Name { get; set; } = default!;
        public List<int> TeamIds { get; set; }
    }
}