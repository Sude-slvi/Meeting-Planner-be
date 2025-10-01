using MeetingPlanning.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Domain.Entities
{
    public class MeetingInvitation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MeetingId { get; set; }
        public Meeting Meeting { get; set; } = default!;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
    }
}
