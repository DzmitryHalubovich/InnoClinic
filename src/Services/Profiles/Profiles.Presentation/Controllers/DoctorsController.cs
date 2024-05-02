using Microsoft.AspNetCore.Mvc;
using Profiles.Contracts.DTOs;

namespace Profiles.Presentation.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    public List<DoctorResponseDTO> Doctors =
    [
        new(Guid.NewGuid(), "Tom", "Jefferson",null,
            DateTime.Now, DateTime.Now, "At work"),  
        new(Guid.NewGuid(), "Sam", "Bridges",null,
            DateTime.Now, DateTime.Now, "Sick Leave"),
        new(Guid.NewGuid(), "Ivan", "Reon","Anatolievich",
            DateTime.Now, DateTime.Now, "Inactive"),  
    ];


    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Doctors);
    }
}
