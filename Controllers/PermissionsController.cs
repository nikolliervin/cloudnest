using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CloudNest.Api.Interfaces;
using CloudNest.Api.Models.Dtos;

namespace CloudNest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : Controller
    {
        private readonly IPermissionsService _permissionsService;
        public PermissionsController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }
        [HttpPost("directory")]
        [Authorize]
        public async Task<IActionResult> SetDirectoryPermissions(DirectoryPermissionsDto directoryPermissionsDto)
        {
            var response = await _permissionsService.SetDirectoryPermissions(directoryPermissionsDto);
            if (!response.Success)
            {
                return BadRequest(response.Error);
            }

            return Ok(response);
        }

    }
}