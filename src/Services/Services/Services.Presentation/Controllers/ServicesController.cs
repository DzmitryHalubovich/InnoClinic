using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Service;
using Services.Services.Abstractions;

namespace Services.Presentation.Controllers;

[ApiController]
[Route("api/services")]
public class ServicesController : ControllerBase
{
    private readonly IServicesService _servicesService;

    public ServicesController(IServicesService servicesService) =>
        _servicesService = servicesService;

    [HttpGet]
    public async Task<IActionResult> GetAllServices()
    {
        var getActiveServicesResult = await _servicesService.GetActiveServicesAsync();

        return getActiveServicesResult.Match<IActionResult>(Ok, notFound => NotFound());
    }

    [HttpGet("{id}", Name = "GetServiceById")]
    public async Task<IActionResult> GetServiceById([FromRoute] Guid id)
    {
        var getServiceByIdResult = await _servicesService.GetServiceByIdAsync(id);

        return getServiceByIdResult.Match<IActionResult>(Ok, notFound => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] ServiceCreateDTO newService, 
        IValidator<ServiceCreateDTO> validator)
    {
        var validationResult = validator.Validate(newService);

        if (validationResult.IsValid)
        {
            var createServiceResult = await _servicesService.CreateServiceAsync(newService);

            return createServiceResult.Match<IActionResult>(
                createdService => CreatedAtRoute("GetServiceById", new { id = createdService.Id }, createdService),
                notFound => NotFound());
        }

        return BadRequest(validationResult.ToDictionary());
    }
}
