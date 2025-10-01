using MeetingPlanning.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.Interfaces.Service
{
    public interface IOrganizationService
    {
        public Task ImportDummyData();
        public Task<IEnumerable<Team>> GetAllTeams();
        public Task<Team> GetTeamById(int id); 

    }
}
