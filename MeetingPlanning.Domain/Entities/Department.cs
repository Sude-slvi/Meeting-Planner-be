using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }=default!;
        public ICollection<Team> Teams { get; set; }=new List<Team>();
    }
}
