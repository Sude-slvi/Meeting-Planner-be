using MeetingPlanning.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.DTOs
{
    public class UpdateInvitationDto
    {
        public Guid Id { get; set; }
        public InvitationStatus Status { get; set; }
    }
}
