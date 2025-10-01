using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Service;
using MeetingPlanning.Application.Security.Claims;
using MeetingPlanning.Application.Services;
using MeetingPlanning.Domain.Entities;
using MeetingPlanning.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MeetingPlanning.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IMeetingRoomService _meetingRoomService;
        private readonly IUserService _userService;
        private readonly ILogger<MeetingController> _logger;

        public MeetingController(
            IMeetingService meetingService,
            IMeetingRoomService meetingRoomService,
            IUserService userService,
            ILogger<MeetingController> logger)
        {
            _meetingService = meetingService;
            _meetingRoomService = meetingRoomService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("CreateMeeting")]
        public async Task<IActionResult> Create([FromBody] CreateMeetingDto dto)
        {
            var username = User.GetUsername();

            var user = await _userService.GetByUsernameAsync(username);

            if (user == null)
                return Unauthorized("Kullanıcı bulunamadı.");

            try
            {
                var meetingId = await _meetingService.CreateMeetingAsync(
                    dto.Title,
                    dto.StartTime,
                    dto.Duration,
                    dto.MeetingRoomId,
                    user.Id,
                    dto.InvitedUserIds);

                return Ok(new { Id = meetingId });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Meeting veritabanına eklenirken hata oluştu");
                return StatusCode(500, new { Message = "Veritabanı hatası oluştu." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Meeting oluşturulurken beklenmeyen bir hata oluştu");
                return StatusCode(500, new { Message = "Sunucuda bir hata oluştu." });
            }
        }

        [HttpGet("GetAllMeetings")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllMeetings()
        {
            var meetings = await _meetingService.GetAllMeetings();
            return Ok(meetings);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMeeting([FromBody] UpdateMeetingDto dto)
        {

            try
            {
                await _meetingService.UpdateMeetingAsync(dto);
                return Ok(new { message = "Toplantı başarıyla güncellendi" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Toplantı bulunamadı" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası oluştu", detail = ex.Message });
            }

        }

        [HttpGet("GetUsersMeetings")]
        public async Task<ActionResult<List<ListMeetingDto>>> GetUsersMeetingsByUsername()
        {
            var username = User.GetUsername();
            var response = await _meetingService.GetUsersMeetingsByUsername(username);

            if (response == null)
                return NotFound("Bu kullanıcıya ait toplantı bulunamadı.");

            return Ok(response);
        }

        [HttpGet("GetTeamMeetings")]
        public async Task<ActionResult<List<ListMeetingDto>>> GetTeamMeetings()
        {
            var username = User.GetUsername();
            var response = await _meetingService.GetTeamMeetingsByUsername(username);

            if (response == null || response.Count == 0)
                return NotFound("Bu kullanıcının takımına ait toplantı bulunamadı.");

            return Ok(response);
        }

        [HttpGet("GetInvitation")]
        public async Task<ActionResult<List<ListMeetingInvitationDto>>> GetAllInvitation()
        {
            var username=User.GetUsername();
            var response = await _meetingService.GetAllInvitation(username);
            if (response == null)
                return NotFound("Yeni bildirim yok.");

            return Ok(response);
        }

        [HttpPut("UpdateInvitation")]
        public async Task<IActionResult> UpdateInvitationStatus([FromBody] UpdateInvitationDto dto)
        {
            try
            {
                await _meetingService.UpdateInvitationStatus(dto);
                return Ok(new { message = "Toplantı başarıyla güncellendi" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Toplantı bulunamadı" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası oluştu", detail = ex.Message });
            }
        }

    }
}
