using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudNest.Api.Models;
using CloudNest.Services;
using CloudNest.Api.Interfaces;

namespace CloudNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUserAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [Authorize]
        [HttpGet("getSettings")]
        public async Task<IActionResult> GetUserSettings()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.GetUserSettings();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [Authorize]
        [HttpGet("getStorageData")]
        public async Task<IActionResult> GetStorage()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.GetStorageData();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}