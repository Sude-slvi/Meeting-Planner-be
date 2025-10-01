using MeetingPlanning.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.Interfaces.Repository
{
    public interface IOrganizationRepository
    {
        Task AddDepartmentsAsync(IEnumerable<Department> departments);
        Task AddTeamsAsync(IEnumerable<Team> teams);

        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(int id);
        Task<Team?> GetTeamByNameAsync(string name);
        Task AddAsync(Team team);
        Task SaveChangesAsync();
    }
}
