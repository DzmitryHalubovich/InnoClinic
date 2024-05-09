using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.Contracts.DTOs.Receptionist;
using Profiles.Services.Abstractions;

namespace Profiles.Presentation.Controllers;

[ApiController]
[Route("api/receptionists")]
public class ReceptionistsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ReceptionistsController(IServiceManager serviceManager) =>
        _serviceManager = serviceManager;

    /// <summary>
    /// Returns list of receptionists
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Returns receptionists succesfully</response>
    /// <response code="404">Returns if there aren't any receptionists in the database</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllReceptionists()
    {
        var receptionists = await _serviceManager.ReceptionistsService.GetAllReceptionistsAsync(false);

        return Ok(receptionists);
    }

    /// <summary>
    /// Returns receptionist by provided id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns receptionists succesfully</response>
    /// <response code="404">Returns if there aren't any receptionists in the database</response>
    [HttpGet("{id}", Name = "GetReceptionistById")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var receptionist = await _serviceManager.ReceptionistsService.GetReceptionistByIdAsync(id, false);

        return Ok(receptionist);
    }

    /// <summary>
    /// Add receptionist in the database
    /// </summary>
    /// <param name="validator"></param>
    /// <param name="newRecepionist"></param>
    /// <returns></returns>
    /// <response code="201">Returns if receptionist was created</response>
    /// <response code="400">Returns if entity was invalid</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddReceptionist(IValidator<ReceptionistCreateDTO> validator, 
        [FromBody] ReceptionistCreateDTO newRecepionist)
    {
        var validationResult = validator.Validate(newRecepionist);

        if (validationResult.IsValid)
        {
            var createdReceptionist = await _serviceManager.ReceptionistsService.CreateReceptionistAsync(newRecepionist);

            return CreatedAtRoute("GetReceptionistById", new { createdReceptionist.Id }, createdReceptionist);
        }

        return BadRequest(validationResult.ToDictionary());
    }

    /// <summary>
    /// Update receptionist by provided id
    /// </summary>
    /// <param name="validator"></param>
    /// <param name="id"></param>
    /// <param name="updatedReceptionist"></param>
    /// <response code="204">Returns if receptionist was updated</response>
    /// <response code="400">Returns if entity was invalid</response>
    /// <response code="404">Returns if there is no receptionist with provided id in the database</response>
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReceptionist(IValidator<ReceptionistUpdateDTO> validator,
        [FromRoute] Guid id, [FromBody] ReceptionistUpdateDTO updatedReceptionist)
    {
        var validationResult = validator.Validate(updatedReceptionist);

        if (validationResult.IsValid)
        {
            await _serviceManager.ReceptionistsService.UpdateReceptionistAsync(id, updatedReceptionist);

            return NoContent();
        }

        return BadRequest(validationResult.ToDictionary());
    }

    /// <summary>
    /// Delete receptionist by provided id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204">Returns if receptionist was deleted</response>
    /// <response code="404">Returns if there is no receptionist with provided id in the database</response>
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReceptionist(Guid id)
    {
        await _serviceManager.ReceptionistsService.DeleteReceptionistAsync(id);

        return NoContent();
    }
}
