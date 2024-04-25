﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Offices.Contracts.DTOs;
using Offices.Services.Abstractions;
using Serilog;

namespace Offices.Presentation.Controllers;

[ApiController]
[Route("api/offices")]
public class OfficesController : ControllerBase
{
    private readonly IOfficesService _officesService;
    private readonly ILogger<OfficesController> _logger;

    public OfficesController(IOfficesService officesService, ILogger<OfficesController> logger)
    {
        _officesService = officesService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOffices()
    {
        _logger.LogInformation("We are in GetAllOffices method");

        var offices = await _officesService.GetAllOfficesAsync();

        Log.Information("Response include {@count} entities.",offices.Count);
        Log.Information("Response: {@Offices}", offices);

        if (offices is null || !offices.Any())
            return NotFound();

        return Ok(offices);
    }

    [HttpGet("{officeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOfficeById([FromRoute] string officeId)
    {
        var office = await _officesService.GetOfficeByIdAsync(officeId);

        return Ok(office);
    }

    [HttpPost]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteOffice([FromRoute] string officeId)
    {
        await _officesService.DeleteOfficeAsync(officeId);
        return NoContent();
    }

    [HttpPut("{officeId}")]
    public async Task<IActionResult> UpdateOffice([FromBody] OfficeUpdateDTO editedOffice, [FromRoute] string officeId)
    {
        await _officesService.UpdateOfficeAsync(officeId, editedOffice);

        return NoContent();
    }
}
