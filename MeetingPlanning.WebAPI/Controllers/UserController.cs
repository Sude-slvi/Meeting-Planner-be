using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Application.Interfaces.Service;
using MeetingPlanning.Application.Security.Claims;
using MeetingPlanning.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingPlanning.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("addUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var user = await _userService.GetOrCreateUserAsync(User);

                if (user == null)
                {
                    var rol = User.GetRoles();
                    if (rol.Contains("admin"))
                    {
                        return Ok();
                    }
                    else
                        return NotFound(new { message = "Kullanıcı bulunamadı" });
                }

                return Ok(user);
            }
            catch(TeamExceptions ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<ListUsersDto>>> GetAllUsers()
        {
            var username = User.GetUsername();
            var user = await _userService.GetByUsernameAsync(username);
            if (user == null)
                return BadRequest("Kullanıcı bulunamadı veya admin olduğu için eklenmedi.");

            return await _userService.GetAllUsersExceptUser(user.Id);
        }
    }
}
