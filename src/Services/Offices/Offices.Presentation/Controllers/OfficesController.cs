using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Offices.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class OfficesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetOffices()
    {
        return Ok("Returns offices.");
    }
}
