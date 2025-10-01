using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.DTOs
{
    public record DepartmentDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
    
}
