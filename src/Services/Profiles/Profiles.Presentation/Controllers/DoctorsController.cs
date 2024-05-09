using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.Contracts.DTOs.Doctor;
using Profiles.Contracts.Pagination;
using Profiles.Services.Abstractions;

namespace Profiles.Presentation.Controllers;

/// <summary>
/// Doctors controller
/// </summary>
[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public DoctorsController(IServiceManager serviceManager) => 
        _serviceManager = serviceManager;

    /// <summary>
    /// Returns list of doctors
    /// </summary>
    /// <param name="parameters">Doctor's full name for searching</param>
    /// <returns>List of doctors in the database</returns>
    /// <response code="200">Returns doctors successfully</response>
    /// <response code="404">Returns if there aren't any doctors in the database</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllDoctors([FromQuery] DoctorsQueryParameters parameters)
    {
        var doctors = await _serviceManager.DoctorsService.GetAllDoctorsAsync(parameters,false);

        return Ok(doctors);
    }

    /// <summary>
    /// Returns doctor by provided id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns doctor successfully</response>
    /// <response code="404">Returns if doctor with provided id was not found</response>
    [HttpGet("{id}", Name = "GetDoctorById")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var doctor = await _serviceManager.DoctorsService.GetDoctorByIdAsync(id, false);

        return Ok(doctor);
    }

    /// <summary>
    /// Add new doctor to the database
    /// </summary>
    /// <param name="validator"></param>
    /// <param name="newDoctor"></param>
    /// <returns></returns>
    /// <response code="201">Returns if doctor was created</response>
    /// <response code="400">Returns if entity was invalid</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddDoctor(IValidator<DoctorCreateDTO> validator, 
        [FromBody] DoctorCreateDTO newDoctor)
    {
        var validationResult = validator.Validate(newDoctor);

        if (validationResult.IsValid)
        {
            var createdDoctor = await _serviceManager.DoctorsService.CreateDoctorAsync(newDoctor);

            return CreatedAtRoute("GetDoctorById", new { createdDoctor.Id }, createdDoctor);
        }

        return BadRequest(validationResult.ToDictionary());
    }

    /// <summary>
    /// Update doctor by provided id
    /// </summary>
    /// <param name="validator"></param>
    /// <param name="id"></param>
    /// <param name="editedDotctor"></param>
    /// <returns></returns>
    /// <response code="204">Returns if doctor was updated</response>
    /// <response code="400">Returns if enitity was invalid</response>
    /// <response code="404">Returns if doctor with provided id was not found in the database</response>
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDoctor(IValidator<DoctorUpdateDTO> validator,
        [FromRoute] Guid id, [FromBody] DoctorUpdateDTO editedDotctor)
    {
        var validationResult = validator.Validate(editedDotctor);

        if (validationResult.IsValid)
        {
            await _serviceManager.DoctorsService.UpdateDoctorAsync(id, editedDotctor);

            return NoContent();
        }

        return BadRequest(validationResult.ToDictionary());
    }

    /// <summary>
    /// Delete doctor by provided id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204">Returns if doctor was deleted</response>
    /// <response code="404">Returns if doctor with provided id was not found in the database</response>
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDoctor([FromRoute] Guid id)
    {
        await _serviceManager.DoctorsService.DeleteDoctorAsync(id);

        return NoContent();
    }
}
