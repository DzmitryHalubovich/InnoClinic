using Microsoft.AspNetCore.Mvc;
using Profiles.Contracts.DTOs.Doctor;
using Profiles.Presentation.Pagination;
using Profiles.Services.Abstractions;

namespace Profiles.Presentation.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public DoctorsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DoctorsQueryParameters parameters)
    {
        var doctors = await _serviceManager.DoctorsService.GetAllDoctorsAsync(parameters,false);

        return Ok(doctors);
    }

    [HttpGet("{doctorId}", Name = "GetDoctorById")]
    public async Task<IActionResult> GetById([FromRoute] Guid doctorId)
    {
        var doctor = await _serviceManager.DoctorsService.GetDoctorByIdAsync(doctorId, false);

        return Ok(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> AddDoctor([FromBody] DoctorCreateDTO newDoctor)
    {
        var createdDoctor = await _serviceManager.DoctorsService.CreateDoctorAsync(newDoctor);

        return CreatedAtRoute("GetDoctorById", new { doctorId = createdDoctor.DoctorId }, createdDoctor);
    }

    [HttpPut("{doctorId}")]
    public async Task<IActionResult> UpdateDoctor([FromRoute] Guid doctorId, [FromBody] DoctorUpdateDTO editedDotctor)
    {
        await _serviceManager.DoctorsService.UpdateDoctorAsync(doctorId, editedDotctor);

        return NoContent();
    }
}
