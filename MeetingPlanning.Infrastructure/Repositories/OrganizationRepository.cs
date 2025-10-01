using MeetingPlanning.Application.Interfaces.Repository;
using MeetingPlanning.Domain.Entities;
using MeetingPlanning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly AppDbContext _context;

        public OrganizationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddDepartmentsAsync(IEnumerable<Department> departments)
        {
            await _context.Departments.AddRangeAsync(departments);
            await _context.SaveChangesAsync();
        }

        public async Task AddTeamsAsync(IEnumerable<Team> teams)
        {
            await _context.Teams.AddRangeAsync(teams);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamById(int id)
        {
            var team= await _context.Teams
                .Include(t=>t.Department)
                .FirstOrDefaultAsync(t=>t.Id==id);

            if (team is null) throw new KeyNotFoundException($"Team {id} not found.");
            return team;
        }

        public async Task<Team?> GetTeamByNameAsync(string name)
        {
            return await _context.Teams.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task AddAsync(Team team) => await _context.Teams.AddAsync(team);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
