using MeetingPlanning.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace MeetingPlanning.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("import")]
        public async Task<IActionResult> ImportDummyData()
        {
            await _organizationService.ImportDummyData();
            return Ok("Dummy organization data imported. ");
        }

        [HttpGet("GetAllTeams")]
        public async Task<IActionResult> GetAllTeams()
        {
           var teams= await _organizationService.GetAllTeams();
            return Ok(teams);
        }

        [HttpGet("teams/{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var team = await _organizationService.GetTeamById(id);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

    }

}
