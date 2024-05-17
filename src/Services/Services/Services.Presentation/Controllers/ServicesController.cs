using Microsoft.AspNetCore.Mvc;

namespace Services.Presentation.Controllers;

[ApiController]
[Route("api/services")]
public class ServicesController : ControllerBase
{
    public ServicesController()
    {
        
    }

    [HttpGet]
    public IActionResult GetAllServices()
    {
        return Ok();
    }
}
