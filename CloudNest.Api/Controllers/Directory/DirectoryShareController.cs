using CloudNest.Api.Interfaces;
using CloudNest.Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CloudNest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectoryShareController : ControllerBase
    {
        private readonly IDirectoryShareService _directoryShareService;

        public DirectoryShareController(IDirectoryShareService directoryShareService)
        {
            _directoryShareService = directoryShareService;
        }

        [HttpPost("share")]
        [Authorize]
        public async Task<IActionResult> ShareDirectory(DirectoryShareDto directoryShareDto)
        {
            var response = await _directoryShareService.ShareDirectoryAsync(directoryShareDto);
            if (!response.Success)
            {
                return BadRequest(response.Error);
            }

            return Ok(response);
        }

        [HttpPost("remove-share")]
        [Authorize]
        public async Task<IActionResult> RemoveShareDirectory(DirectoryShareDto directoryShareDto)
        {
            var response = await _directoryShareService.RemoveShareDirectoryAsync(directoryShareDto);
            if (!response.Success)
            {
                return BadRequest(response.Error);
            }

            return Ok(response);
        }

        [HttpGet("get-shared")]
        public async Task<IActionResult> GetSharedDirectories([FromQuery] Guid userId)
        {
            var response = await _directoryShareService.GetSharedDirectoriesAsync(userId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
