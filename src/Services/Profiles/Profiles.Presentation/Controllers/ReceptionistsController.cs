using Microsoft.AspNetCore.Mvc;
using Profiles.Contracts.DTOs.Receptionist;
using Profiles.Services.Abstractions;

namespace Profiles.Presentation.Controllers;

[ApiController]
[Route("api/receptionists")]
public class ReceptionistsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ReceptionistsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var receptionists = await _serviceManager.ReceptionistsService.GetAllReceptionistsAsync(false);

        if (!receptionists.Any())
        {
            return NotFound("Empty collection");
        }

        return Ok(receptionists);
    }

    [HttpGet("{receptionistId}", Name = "GetReceptionistById")]
    public async Task<IActionResult> GetById([FromRoute] Guid receptionistId)
    {
        var receptionist = await _serviceManager.ReceptionistsService.GetReceptionistByIdAsync(receptionistId, false);

        return Ok(receptionist);
    }

    [HttpPost]
    public async Task<IActionResult> AddReceptionist([FromBody] ReceptionistCreateDTO newRecepionist)
    {
        var createdReceptionist = await _serviceManager.ReceptionistsService.CreateReceptionistAsync(newRecepionist);

        return CreatedAtRoute("GetReceptionistById", new { receptionistId = createdReceptionist.ReceptionistId }, createdReceptionist);
    }

    [HttpPut("{receptionistId}")]
    public async Task<IActionResult> UpdateReceptionist([FromRoute] Guid receptionistId,[FromBody] ReceptionistUpdateDTO updatedReceptionist)
    {
        await _serviceManager.ReceptionistsService.UpdateReceptionistAsync(receptionistId, updatedReceptionist);

        return NoContent();
    }

    [HttpDelete("{receptionistId}")]
    public async Task<IActionResult> DeleteReceptionist(Guid receptionistId)
    {
        await _serviceManager.ReceptionistsService.DeleteReceptionistAsync(receptionistId);

        return NoContent();
    }
}
