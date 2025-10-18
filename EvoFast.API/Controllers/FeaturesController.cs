using Microsoft.AspNetCore.Mvc;

namespace EvoFast.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeaturesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromServices] IConfiguration configuration)
    {
        var showForGuest = configuration.GetValue<bool>("ShowForGuest", false);
        return Ok(new { showForGuest });
    }
}