using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Profiles.API.Extensions;
using Profiles.Contracts.DTOs.Doctor;
using Profiles.Contracts.DTOs.Patient;
using Profiles.Contracts.DTOs.Receptionist;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;
using Profiles.Infrastructure.Repositories;
using Profiles.Presentation.Validators;
using Profiles.Services.Abstractions;
using Profiles.Services.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Logging.ClearProviders();

ValidatorOptions.Global.LanguageManager.Enabled = false;

ConfigureServices(builder.Services);

var app = builder.Build();

var logger = app.Services.GetRequiredService<Serilog.ILogger>();

app.ConfigureExceptionHandler(logger);

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
    services.AddScoped<IValidator<DoctorCreateDTO>, DoctorCreateValidator>();
    services.AddScoped<IValidator<DoctorUpdateDTO>, DoctorUpdateValidator>();
    services.AddScoped<IValidator<PatientCreateDTO>, PatientCreateValidator>();
    services.AddScoped<IValidator<PatientUpdateDTO>, PatientUpdateValidator>();    
    services.AddScoped<IValidator<ReceptionistCreateDTO>, ReceptionistCreateValidator>();
    services.AddScoped<IValidator<ReceptionistUpdateDTO>, ReceptionistUpdateValidator>();
    services.AddScoped<IRepositoryManager, RepositoryManager>();
    services.AddScoped<IServiceManager, ServiceManager>();
    services.AddScoped<IDoctorsService, DoctorsService>();
    services.AddScoped<IPatientsService, PatientsService>();
    services.AddScoped<IDoctorsRepository, DoctorsRepository>();
    services.AddScoped<IAccountsRepository, AccountRepository>();
    services.AddScoped<IPatientsRepository, PatientsRepository>();
    services.AddAutoMapper(typeof(MapperProfile));
    services.AddHttpClient();
    services.AddDbContext<ProfilesDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
    services.AddControllers()
        .AddApplicationPart(typeof(Profiles.Presentation.Controllers.DoctorsController).Assembly);
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "ProfilesAPI", Version = "v1" });

        var xmlFilename = Path.Combine(AppContext.BaseDirectory, "Profiles.Presentation.xml");
        options.IncludeXmlComments(xmlFilename);
    });
}