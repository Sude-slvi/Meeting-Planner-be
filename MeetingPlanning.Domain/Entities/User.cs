using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MeetingPlanning.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;

        public int TeamId { get; set; }
        public Team Team { get; set; } = default!;

        [JsonIgnore]
        public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
        public ICollection<MeetingInvitation> MeetingInvitations { get; set; } = new List<MeetingInvitation>();

    }
}
