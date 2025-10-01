using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!; 

        public ICollection<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>();
    }
}
