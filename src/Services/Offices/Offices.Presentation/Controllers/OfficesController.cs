using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Offices.Services.Abstractions;

namespace Offices.Presentation.Controllers;

[ApiController]
[Route("api/offices")]
public class OfficesController : ControllerBase
{
    private readonly IOfficesService _officesService;

    public OfficesController(IOfficesService officesService)
    {
        _officesService = officesService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOffices()
    {
        var offices = await _officesService.GetAllOfficesAsync();

        if (offices is null || !offices.Any())
            return NotFound();

        return Ok(offices);
    }
}
