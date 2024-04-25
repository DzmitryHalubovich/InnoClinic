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

    /// <summary>
    /// Returns list of offices
    /// </summary>
    /// <returns>Some result</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOffices()
    {
        var offices = await _officesService.GetAllOfficesAsync();

        if (offices is null || !offices.Any())
            return NotFound();

        return Ok(offices);
    }


    /// <summary>
    /// Returns office by id
    /// </summary>
    /// <param name="officeId"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /GetOfficeById
    ///     {
    ///         "id": "6628bfbfb2cf06aabe117b7d",
    ///         "address": "Some address",
    ///         "photo_Id": "6628bfbfb2cf06aabe117b7d",
    ///         "registry_phone_number": "+123456785894",
    ///         "isActive": true
    ///     }
    /// 
    /// </remarks>
    /// <returns>Office with requested id</returns>
    /// <response code="200">Returns office successfully</response>
    /// <response code="404">If office with requested id doesn't exist in the database</response>
    [HttpGet("{officeId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOfficeById([FromRoute] string officeId)
    {
        var office = await _officesService.GetOfficeByIdAsync(officeId);

        return Ok(office);
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    [HttpDelete("{officeId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteOffice([FromRoute] string officeId)
    {
        await _officesService.DeleteOfficeAsync(officeId);
        return NoContent();
    }

    [HttpPut("{officeId}")]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateOffice(IValidator<OfficeUpdateDTO> validator,
        [FromBody] OfficeUpdateDTO editedOffice, [FromRoute] string officeId)
    {
        var validationResult = validator.Validate(editedOffice);

        if (validationResult.IsValid)
        {
            await _officesService.UpdateOfficeAsync(officeId, editedOffice);
            return NoContent();
        }

        return BadRequest(validationResult.ToDictionary());
    }
}
