using FluentValidation;
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

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Logging.ClearProviders();

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
    services.AddScoped<IValidator<OfficeCreateDTO>, OfficeCreateValidator>();
    services.AddScoped<IValidator<OfficeUpdateDTO>, OfficeUpdateValidator>();
    services.AddScoped<IOfficesRepository, OfficesRepository>();
    services.AddScoped<IOfficesService, OfficesService>();
    services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));
    services.AddAutoMapper(typeof(MapperProfile));
    services.AddControllers()
    .AddApplicationPart(typeof(Offices.Presentation.Controllers.OfficesController).Assembly);
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "OfficeAPI",
            Version = "v1"
        });

        var xmlFilename = Path.Combine(AppContext.BaseDirectory, "Offices.Presentation.xml");
        options.IncludeXmlComments(xmlFilename);
    });
}