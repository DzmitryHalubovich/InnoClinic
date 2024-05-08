using Microsoft.EntityFrameworkCore;
using Profiles.API.Extensions;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;
using Profiles.Infrastructure.Repositories;
using Profiles.Services.Abstractions;
using Profiles.Services.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Logging.ClearProviders();

builder.Services.AddControllers();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IDoctorsService, DoctorsService>();
builder.Services.AddScoped<IPatientsService, PatientsService>();
builder.Services.AddScoped<IDoctorsRepository, DoctorsRepository>();
builder.Services.AddScoped<IAccountsRepository, AccountRepository>();
builder.Services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddHttpClient();

builder.Services.AddDbContext<ProfilesDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddControllers()
.AddApplicationPart(typeof(Profiles.Presentation.Controllers.DoctorsController).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
