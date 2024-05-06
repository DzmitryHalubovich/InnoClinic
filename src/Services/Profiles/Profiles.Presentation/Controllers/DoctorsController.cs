using Microsoft.AspNetCore.Mvc;
using Profiles.Contracts.DTOs;
using Profiles.Presentation.Pagination;
using Profiles.Services.Abstractions;

namespace Profiles.Presentation.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorsService _doctorsService;

    public DoctorsController(IDoctorsService doctorsService)
    {
        _doctorsService = doctorsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DoctorsQueryParameters parameters)
    {
        var doctors = await _doctorsService.GetAllDoctorsAsync(parameters,false);

        return Ok(doctors);
    }

    [HttpGet("{doctorId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid doctorId)
    {
        var doctor = await _doctorsService.GetDoctorByIdAsync(doctorId, false);

        return Ok(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> AddDoctor([FromBody] DoctorCreateDTO newDoctor)
    {
        var createdDoctor = await _doctorsService.CreateDoctorAsync(newDoctor);

        return Ok(createdDoctor);
    }
}
