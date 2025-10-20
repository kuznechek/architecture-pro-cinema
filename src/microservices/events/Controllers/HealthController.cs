using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EventsService.Controllers;

[ApiController]
[Route("api/events")]
public class HealthController : ControllerBase
{ 

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { Status = true });
    }
}