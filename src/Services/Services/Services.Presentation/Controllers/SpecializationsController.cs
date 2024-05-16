using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Specialization;
using Services.Services.Abstractions;

namespace Services.Presentation.Controllers;

[ApiController]
[Route("api/specializations")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationsService _specializationService;

    public SpecializationsController(ISpecializationsService specializationsService)
    {
        _specializationService = specializationsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSpecializations()
    {
        var specResponse = await _specializationService.GetAllSpecializationsAsync();

        if (specResponse is null)
        {
            return NotFound();
        }

        return Ok(specResponse);
    }

    [HttpGet("{id}", Name = "GetSpecializationById")]
    public async Task<IActionResult> GetSpecializationById([FromRoute] int id)
    {
        var specResponse = await _specializationService.GetSpecializationByIdAsync(id);

        if (specResponse is null)
        {
            return NotFound();
        }

        return Ok(specResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecialization([FromBody] SpecializationCreateDTO newSpecialization, 
        IValidator<SpecializationCreateDTO> validator)
    {
        var validationResult = validator.Validate(newSpecialization);

        if(validationResult.IsValid)
        {
            var specResponse = await _specializationService.CreateSpecializationAsync(newSpecialization);

            return CreatedAtRoute("GetSpecializationById", new { id = specResponse.Id },specResponse);
        }

        return BadRequest(validationResult.ToDictionary());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpecialization([FromRoute] int id, 
        [FromBody] SpecializationUpdateDTO editedSpecialization)
    {
        return NoContent();
    }
}
