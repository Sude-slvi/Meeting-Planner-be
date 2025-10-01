using MeetingPlanning.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.DTOs
{
    public class ListUsersDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string TeamName { get; set; } = default!;
        public InvitationStatus Status { get; set; }

    }
}
