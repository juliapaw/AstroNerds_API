using AstroNerds_API.DbContexts;
using AstroNerds_API.Repositories;
using AstroNerds_API.Services;
using AutoMapper;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/zodiacinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyy-MM-dd")); // convert DateTime format
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

//connection
var connectionString = builder.Configuration.GetConnectionString("DefaulConnection");

//context
builder.Services.AddDbContext<ZodiacContext>(
    DbContextOptions => DbContextOptions.UseSqlServer(connectionString));

builder.Services.AddScoped<IZodiacRepository, ZodiacRepository>();
builder.Services.AddScoped<IHoroscopeRepository, HoroscopeRepository>();
builder.Services.AddScoped<IDailyHoroscopeFileContentRepository, DailyHoroscopeFileContentRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
