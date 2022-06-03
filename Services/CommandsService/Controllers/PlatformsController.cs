using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controller;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    public PlatformsController() { }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inboud Post # Command");
        return Ok("Inbound test of from platforms controller");
    }


}