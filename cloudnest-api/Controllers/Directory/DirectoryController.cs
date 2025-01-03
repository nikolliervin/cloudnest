using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudNest.Api.Interfaces;
using CloudNest.Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CloudNest.Api.Helpers;

namespace CloudNest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectoryController : ControllerBase
    {
        private readonly IDirectoryService _directoryService;

        public DirectoryController(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        [HttpPost("create")]
        [Permissions("Add")]
        public async Task<IActionResult> CreateDirectory([FromBody] DirectoryDto directoryDto)
        {
            if (directoryDto == null)
            {
                return BadRequest(ResponseMessages.InvalidDirectoryData);
            }

            var directoryResponse = await _directoryService.CreateDirectoryAsync(directoryDto);

            if (!directoryResponse.Success)
            {
                return BadRequest(directoryResponse);
            }

            return Ok(directoryResponse);
        }

        [HttpPut("update")]
        [Authorize]
        [Permissions("Update")]
        public async Task<IActionResult> UpdateDirectory([FromBody] DirectoryDto directoryDto)
        {
            if (directoryDto == null)
            {
                return BadRequest(ResponseMessages.InvalidDirectoryData);
            }

            var directoryResponse = await _directoryService.UpdateDirectoryAsync(directoryDto);

            if (!directoryResponse.Success)
            {
                return BadRequest(directoryResponse);
            }

            return Ok(directoryResponse);
        }

        [HttpDelete("delete")]
        [Authorize]
        [Permissions("Delete")]
        public async Task<IActionResult> DeleteDirectory([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(ResponseMessages.InvalidDirectoryId);
            }

            var response = await _directoryService.DeleteDirectoryAsync(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpGet("directory")]
        [Authorize]
        [Permissions("View")]
        public async Task<IActionResult> GetDirectory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(ResponseMessages.InvalidDirectoryId);
            }

            var directoryResponse = await _directoryService.GetDirectoryAsync(id);

            if (!directoryResponse.Success)
            {
                return NotFound(directoryResponse.Message);
            }

            return Ok(directoryResponse);
        }

        [HttpGet("directories")]
        [Authorize]
        public async Task<IActionResult> GetUserDirectories()
        {
            var directoriesResponse = await _directoryService.GetUserDirectoriesAsync();

            if (!directoriesResponse.Success || directoriesResponse.Data == null || !directoriesResponse.Data.Any())
            {
                return NotFound(ResponseMessages.DirectoryNotFound);
            }

            return Ok(directoriesResponse);
        }
    }
}
