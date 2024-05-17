using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> GetSpecializations()
    {
        var getSpecializationResponse = await _specializationsService.GetAllSpecializationsAsync();

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

        if(validationResult.IsValid)
        {
            var specResponse = await _specializationsService.CreateSpecializationAsync(newSpecialization);

            return CreatedAtRoute("GetSpecializationById", new { id = specResponse.Id },specResponse);
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
}
