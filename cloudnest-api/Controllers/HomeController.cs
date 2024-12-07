using Microsoft.AspNetCore.Mvc;

namespace CloudNest.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "Welcome to CloudNest API!" });
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { Status = "API is up and running!" });
        }
    }
}