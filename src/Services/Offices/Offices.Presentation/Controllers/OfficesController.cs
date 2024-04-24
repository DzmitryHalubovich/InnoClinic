using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Offices.Contracts.DTOs;
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

        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOffice(IValidator<OfficeCreateDTO> validator,
        [FromBody] OfficeCreateDTO newOffice)
    {
        var validationResult = validator.Validate(newOffice);
        
        if (validationResult.IsValid)
        {
            await _officesService.AddNewOfficeAsync(newOffice);
            return Ok(newOffice);
        }

        return BadRequest(validationResult.ToDictionary());
    }
}
