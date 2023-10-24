using Microsoft.AspNetCore.Mvc;

namespace RentalService.Server.Controllers;

[ApiController]
[Route("/manage")]
public class ManageController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult GetHealth()
    {
        return Ok();
    }
}