using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Service;
using Services.Contracts.Specialization;
using Services.Domain.Interfaces;
using Services.Infrastructure.Data;
using Services.Infrastructure.Repositories;
using Services.Presentation.Validators;
using Services.Services;
using Services.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddAutoMapper(typeof(MapperProfile));
    services.AddScoped<IValidator<SpecializationCreateDTO>, SpecializationCreateValidator>();
    services.AddScoped<IValidator<SpecializationUpdateDTO>, SpecializationUpdateValidator>();
    services.AddScoped<IValidator<ServiceCreateDTO>, ServiceCreateValidator>();
    services.AddScoped<IServicesRepository, ServicesRepository>();
    services.AddScoped<ISpecializationsRepository, SpecializationsRepository>();
    services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
    services.AddScoped<IServicesService, ServicesService>();
    services.AddScoped<ISpecializationsService, SpecializationsService>();
    services.AddDbContext<ServicesDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
