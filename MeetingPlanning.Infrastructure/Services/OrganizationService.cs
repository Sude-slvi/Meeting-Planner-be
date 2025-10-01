using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Repository;
using MeetingPlanning.Application.Interfaces.Service;
using MeetingPlanning.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly HttpClient _httpClient;
        public OrganizationService(IOrganizationRepository organizationRepository, IHttpClientFactory httpClientFactory)
        {
            _organizationRepository = organizationRepository;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
           return await _organizationRepository.GetAllTeams();
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await _organizationRepository.GetTeamById(id);
        }

        public async Task ImportDummyData()
        {
            var departmentsDto = await _httpClient.GetFromJsonAsync<List<DepartmentDto>>("https://68afea6.mockapi.io/api/users/department");
            var teamsDto = await _httpClient.GetFromJsonAsync<List<TeamDto>>("https://68afea6.mockapi.io/api/users/team");
        
            var departments = departmentsDto.Select(d=> new Department { Id= int.Parse(d.Id), Name=d.Name }).ToList();
            var teams = teamsDto.Select(t => new Team { Id = int.Parse(t.Id), Name = t.Name, DepartmentId = int.Parse(t.DepartmentId) }).ToList();
        
            await _organizationRepository.AddDepartmentsAsync(departments);
            await _organizationRepository.AddTeamsAsync(teams);
        }
    }
}
