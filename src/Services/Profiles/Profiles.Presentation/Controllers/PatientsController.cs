using Microsoft.AspNetCore.Mvc;
using Profiles.Contracts.DTOs.Patient;
using Profiles.Services.Abstractions;

namespace Profiles.Presentation.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public PatientsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        var patients = await _serviceManager.PatientsService.GetAllPatientsAsync(false);

        return Ok(patients);
    }

    [HttpGet("{patientId}", Name = "GetPatientById")]
    public async Task<IActionResult> GetPatientById(Guid patientId)
    {
        var patient = await _serviceManager.PatientsService.GetPatientByIdAsync(patientId, false);

        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> AddPatient([FromBody] PatientCreateDTO newPatient)
    {
        var createdPatient = await _serviceManager.PatientsService.CreatePatientAsync(newPatient);

        return CreatedAtAction("GetPatientById", new { patientId = createdPatient.PatientId }, createdPatient);
    }

    [HttpDelete("{patientId}")]
    public async Task<IActionResult> DeletePatient(Guid patientId)
    {
        await _serviceManager.PatientsService.DeletePatientAsync(patientId);

        return NoContent();
    }
}
