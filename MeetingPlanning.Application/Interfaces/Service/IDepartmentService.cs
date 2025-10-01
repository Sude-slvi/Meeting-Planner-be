using MeetingPlanning.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.Interfaces.Service
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetDepartmentsAsync(CancellationToken ct);
    }
}
