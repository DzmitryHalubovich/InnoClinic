using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Filtering;
using Services.Contracts.Specialization;
using Services.Services.Abstractions;

namespace Services.Presentation.Controllers;

[ApiController]
[Route("api/specializations")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationsService _specializationsService;

    public SpecializationsController(ISpecializationsService specializationsService)
    {
        _specializationsService = specializationsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSpecializations([FromQuery] SpecializationsQueryParameters queryParameters)
    {
        var getSpecializationResponse = await _specializationsService.GetAllSpecializationsAsync(queryParameters);

        return getSpecializationResponse.Match<IActionResult>(Ok, notFound => NotFound());
    }

    [HttpGet("{id}", Name = "GetSpecializationById")]
    public async Task<IActionResult> GetSpecializationById([FromRoute] int id)
    {
        var getSpecializationResponse = await _specializationsService.GetSpecializationByIdAsync(id);

        return getSpecializationResponse.Match<IActionResult>(Ok, notFound => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecialization([FromBody] SpecializationCreateDTO newSpecialization,
        IValidator<SpecializationCreateDTO> validator)
    {
        var validationResult = validator.Validate(newSpecialization);

        if (validationResult.IsValid)
        {
            var specResponse = await _specializationsService.CreateSpecializationAsync(newSpecialization);

            return CreatedAtRoute("GetSpecializationById", new { id = specResponse.Id }, specResponse);
        }

        return BadRequest(validationResult.ToDictionary());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpecialization([FromRoute] int id,
        [FromBody] SpecializationUpdateDTO editedSpecialization, IValidator<SpecializationUpdateDTO> validator)
    {
        var validationResult = validator.Validate(editedSpecialization);

        if (validationResult.IsValid)
        {
            var updateSpecializationResult =
                await _specializationsService.UpdateSpecializationAsync(id, editedSpecialization);

            return updateSpecializationResult.Match<IActionResult>(success => NoContent(), notFound => NotFound());
        }

        return BadRequest(validationResult.ToDictionary());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecialization([FromRoute] int id)
    {
        var specializationDeleteResult = await _specializationsService.DeleteSpecializationAsync(id);

        return specializationDeleteResult.Match<IActionResult>(success => NoContent(), notFound => NotFound());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] SpecializationUpdateDTO editedSpecialization)
    {
        var changeStatusResult = await _specializationsService.ChangeSpecializationStatusAsync(id, editedSpecialization);

        return changeStatusResult.Match<IActionResult>(success => NoContent(), notFound => NotFound());
    }
}
