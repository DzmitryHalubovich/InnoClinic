using Offices.Domain.Interfaces;
using Offices.Infrastructure;
using Offices.Infrastructure.Repositories;
using Offices.Services.Abstractions;
using Offices.Services.Services;

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
    builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));
    builder.Services.AddScoped<IOfficesRepository, OfficesRepository>();
    builder.Services.AddScoped<IOfficesService, OfficesService>();
    builder.Services.AddAutoMapper(typeof(MapperProfile));
    builder.Services.AddControllers()
    .AddApplicationPart(typeof(Offices.Presentation.Controllers.OfficesController).Assembly);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}