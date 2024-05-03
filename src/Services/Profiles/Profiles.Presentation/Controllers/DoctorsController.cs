using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Get()
    {
        var doctors = await _doctorsService.GetAllDoctorsAsync();

        return Ok(doctors);
    }

    [HttpGet("{doctorId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid doctorId)
    {
        var doctor = await _doctorsService.GetDoctorByIdAsync(doctorId);

        return Ok(doctor);
    }
}
