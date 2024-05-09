using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.Contracts.DTOs.Patient;
using Profiles.Contracts.Pagination;
using Profiles.Services.Abstractions;

namespace Profiles.Presentation.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public PatientsController(IServiceManager serviceManager) =>
        _serviceManager = serviceManager;

    /// <summary>
    /// Returns list of patients
    /// </summary>
    /// <param name="queryParameters"></param>
    /// <response code="204">Returns patients succesfully</response>
    /// <response code="404">Returns if there aren't any patients in the database</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPatients([FromQuery] PatientsQueryParameters queryParameters)
    {
        var patients = await _serviceManager.PatientsService.GetAllPatientsAsync(queryParameters, false);

        return Ok(patients);
    }

    /// <summary>
    /// Returns patient by provided id
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Returns patient succesfully</response>
    /// <response code="404">Returns if patient with provided id was not found in the database</response>
    [HttpGet("{id}", Name = "GetPatientById")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatientById(Guid id)
    {
        var patient = await _serviceManager.PatientsService.GetPatientByIdAsync(id, false);

        return Ok(patient);
    }

    /// <summary>
    /// Add new patient to the database
    /// </summary>
    /// <param name="validator"></param>
    /// <param name="newPatient"></param>
    /// <response code="201">Returns if patient was created</response>
    /// <response code="400">Returns if entity was invalid</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPatient(IValidator<PatientCreateDTO> validator,
        [FromBody] PatientCreateDTO newPatient)
    {
        var validationResult = validator.Validate(newPatient);

        if (validationResult.IsValid)
        {
            var createdPatient = await _serviceManager.PatientsService.CreatePatientAsync(newPatient);

            return CreatedAtAction("GetPatientById", new { createdPatient.Id }, createdPatient);
        }

        return BadRequest(validationResult.ToDictionary());
    }

    /// <summary>
    /// Update patient by provided id
    /// </summary>
    /// <param name="validator"></param>
    /// <param name="id"></param>
    /// <param name="patientUpdate"></param>
    /// <returns></returns>
    /// <response code="204">Returns if patient was updated</response>
    /// <response code="400">Returns if entity was invalid</response>
    /// <response code="404">Returns if there is no patient with provided id in the database</response>
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePatient(IValidator<PatientUpdateDTO> validator,
        [FromRoute] Guid id, [FromBody] PatientUpdateDTO patientUpdate)
    {
        var validationResult = validator.Validate(patientUpdate);

        if (validationResult.IsValid)
        {
            await _serviceManager.PatientsService.UpdatePatientAsync(id, patientUpdate);

            return NoContent();
        }

        return BadRequest(validationResult.IsValid);
    }

    /// <summary>
    /// Delete patient by provided id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204">Returns if patient was deleted</response>
    /// <response code="404">Returns if there is no patient with provided id in the database</response>
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePatient([FromRoute] Guid id)
    {
        await _serviceManager.PatientsService.DeletePatientAsync(id);

        return NoContent();
    }
}
