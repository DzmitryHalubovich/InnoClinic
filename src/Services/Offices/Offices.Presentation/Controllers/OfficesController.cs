﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Offices.Contracts.DTOs;
using Offices.Services.Abstractions;

namespace Offices.Presentation.Controllers;

/// <summary>
/// Office controller
/// </summary>
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
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /GetAllOffices
    ///     [
    ///         {
    ///             "id": "6628bfbfb2cf06aabe117b7d",
    ///             "address": "Some address1",
    ///             "photoId": "6628bfbfb2cf06aabe117b7d",
    ///             "registryPhoneNumber": "+123456785894",
    ///             "isActive": true
    ///         },
    ///         {
    ///             "id": "6638bfbfb4cf06acbe118b7w",
    ///             "address": "Some address2",
    ///             "photoId": "6628bfbma2cf34aabe117b7d",
    ///             "registryPhoneNumber": "+987654321214",
    ///             "isActive": false
    ///         },
    ///         ...
    ///     ]
    /// 
    /// </remarks>
    /// <returns>List of all offices in the database</returns>
    /// <response code="200">Returns offices successfully</response>
    /// <response code="404">Returns if there aren't any offices in the database</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOffices()
    {
        var offices = await _officesService.GetAllOfficesAsync();

        if (!offices.Any())
        {
            return NotFound();
        }

        return Ok(offices);
    }


    /// <summary>
    /// Returns office by the specified id
    /// </summary>
    /// <param name="officeId">Spesified id of the office in the database</param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /GetOfficeById
    ///     {
    ///         "id": "6628bfbfb2cf06aabe117b7d",
    ///         "address": "Some address",
    ///         "photoId": "6628bfbfb2cf06aabe117b7d",
    ///         "registryPhoneNumber": "+123456785894",
    ///         "isActive": true
    ///     }
    /// 
    /// </remarks>
    /// <returns>Office by the spesified id</returns>
    /// <response code="200">Returns office successfully</response>
    /// <response code="404">Returns if office with spesified id doesn't exist in the database</response>
    [HttpGet("{officeId}", Name = "GetOfficeById")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOfficeById([FromRoute] string officeId)
    {
        var office = await _officesService.GetOfficeByIdAsync(officeId);

        return Ok(office);
    }


    /// <summary>
    /// Add new office to the database
    /// </summary>
    /// <param name="validator">Validator from DI container</param>
    /// <param name="newOffice">Entity from the client</param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /AddOffice
    ///     {
    ///         "address": "Some address",
    ///         "photoId": "6628bfbfb2cf06aabe117b7d",
    ///         "registryPhoneNumber": "+123456785894",
    ///         "isActive": true
    ///     }
    /// 
    /// </remarks>
    /// <response code="201">Returns if office was created</response>
    /// <response code="400">Returns if entity is invalid</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOffice(IValidator<OfficeCreateDTO> validator,
        [FromBody] OfficeCreateDTO newOffice)
    {
        var validationResult = validator.Validate(newOffice);
        
        if (validationResult.IsValid)
        {
            var createdOffice = await _officesService.AddNewOfficeAsync(newOffice);

            return CreatedAtRoute("GetOfficeById",  new { officeId = createdOffice.Id }, createdOffice);
        }

        return BadRequest(validationResult);
    }


    /// <summary>
    /// Deletes the office by the specified id
    /// </summary>
    /// <param name="officeId">Spesified id of the office that should be deleted</param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /DeleteOfficeById
    ///     {
    ///         "string": "6628bfbfb2cf06aabe117b7d",
    ///     }
    /// 
    /// </remarks>
    /// <response code="204">Returns if office was deleted</response>
    /// <response code="404">Returns if office with {officeId} was't found in the database</response>
    [HttpDelete("{officeId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOfficeById([FromRoute] string officeId)
    {
        await _officesService.DeleteOfficeAsync(officeId);

        return NoContent();
    }


    /// <summary>
    /// Update office by the specified id
    /// </summary>
    /// <param name="validator">Validator from DI container</param>
    /// <param name="editedOffice">New office entity for replacement</param>
    /// <param name="officeId">Spesified id of office that should be updated from route</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     Put /UpdateOfficeById
    ///     {
    ///        "string": 6628bfbfb2cf06aabe117b7d
    ///     },
    ///     {
    ///         "address": "Some address",
    ///         "photoId": "6628bfbfb2cf06aabe117b7d",
    ///         "registryPhoneNumber": "+123456785894",
    ///         "isActive": true
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Returns if office was updated successfully</response>
    /// <response code="404">Returns if the office with {officeId} was't found in the database</response>
    [HttpPut("{officeId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOfficeById(IValidator<OfficeUpdateDTO> validator,
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
