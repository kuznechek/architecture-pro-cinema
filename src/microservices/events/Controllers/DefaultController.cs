using EventsService.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace events.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class DefaultController : ControllerBase
    { 

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                Status = true
            });
        }
    }
}
