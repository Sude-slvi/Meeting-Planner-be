using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Domain.Entities
{
    public class MeetingRoom
    {
        public MeetingRoom()
        {
            Id= Guid.NewGuid();
        }
        public Guid Id { get; set; }
        //default!= name null olmayacak bunu mutlaka dolduracağım anlamına geliyor
        public string Name { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;

        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
    }
}
