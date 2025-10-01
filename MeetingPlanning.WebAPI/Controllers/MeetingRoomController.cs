using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Service;
using MeetingPlanning.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingPlanning.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingRoomController : ControllerBase
    {
        private readonly IMeetingRoomService _service;

        public MeetingRoomController(IMeetingRoomService service)
        {
            _service = service;
        }

        [HttpGet("GetAllMeetingRooms")]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            var rooms = await _service.GetAll();
            return Ok(rooms);
        }

        [HttpGet("GetMeetingRoomById/{id:guid}")]
        public async Task<ActionResult<MeetingRoom>> GetMeetingRoomById(Guid id)
        {
            var room = await _service.GetMeetingRoomById(id);
            if (room == null)
                return NotFound();  // 404 döner
            return Ok(room);       // 200 ve JSON döner
        }

        [Authorize(Roles ="admin")]
        [HttpPost("CreateMeetingRoom")]
        public async Task<ActionResult<MeetingRoom>> CreateMeetingRoom([FromBody] MeetingRoomDto roomDto)
        {
            var createdRoom = await _service.CreateMeetingRoom(roomDto);
            var result = new ListMeetingRoomWithTeamDto
            {
                Id = createdRoom.Id,
                Name = createdRoom.Name,
                TeamNames = createdRoom.Teams.Select(t => t.Name).ToList()
            };
            return Ok(result);
        }

        [HttpGet("GetMeetingRoomByTeam")]
        public async Task<ActionResult<List<ListMeetingRoomWithTeamDto>>> GetMeetingRoomByTeam()
        {
            try
            {
                var result = await _service.GetMeetingRoomByTeam(User);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

}

