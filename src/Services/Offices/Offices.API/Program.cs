using FluentValidation;
using Newtonsoft.Json;
using Offices.API.Extensions;
using Offices.Contracts.DTOs;
using Offices.Domain.Interfaces;
using Offices.Infrastructure;
using Offices.Infrastructure.Repositories;
using Offices.Presentation.Validators;
using Offices.Services.Abstractions;
using Offices.Services.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Destructure.ByTransforming<OfficeResponseDTO>(offices => JsonConvert.SerializeObject(offices))
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var serilogLogger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.ClearProviders();

builder.Logging.AddSerilog(serilogLogger);

ConfigureServices(builder.Services);

var app = builder.Build();

var logger = app.Services.GetRequiredService<Serilog.ILogger>();
app.ConfigureExceptionHandler(logger);

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
    services.AddScoped<IValidator<OfficeCreateDTO>, OfficeValidator>();
    services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));
    services.AddScoped<IOfficesRepository, OfficesRepository>();
    services.AddScoped<IOfficesService, OfficesService>();
    services.AddAutoMapper(typeof(MapperProfile));
    services.AddControllers()
    .AddApplicationPart(typeof(Offices.Presentation.Controllers.OfficesController).Assembly);
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}