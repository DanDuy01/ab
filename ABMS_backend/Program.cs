using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICmbAccountManagementRepository, CmbAccountManagementService>();
builder.Services.AddScoped<IUtilityManagementRepository, UtilityManagementService>();
builder.Services.AddScoped<IResidentAccountManagementRepository, ResidentAccountManagementService>();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
});

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CORSPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed((host) => true));
});
builder.Services.AddDbContext<abmsContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("value"),
        new MySqlServerVersion(new Version(11, 3, 2)),
        builder => builder.EnableRetryOnFailure()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CORSPolicy");
app.UseAuthorization();


app.MapControllers();

app.Run();