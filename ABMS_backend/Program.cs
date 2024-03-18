using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IAccountManagementRepository, AccountManagementService>();
builder.Services.AddScoped<IUtilityManagementRepository, UtilityManagementService>();
builder.Services.AddScoped<ILoginAccount, LoginService>();
builder.Services.AddScoped<IRoomInformationRepository, RoomInformationService>();
builder.Services.AddScoped<IMemberManagerRepository, MemberManagerService>();
builder.Services.AddScoped<IReservationManagementRepository, ReservationManagementService>();
builder.Services.AddScoped<IVisitorManagementRepository, VisitorManagementService>();
builder.Services.AddScoped<IElevatorRepository, ElevatorService>();
builder.Services.AddScoped<IConstructionManagementRepository, ConstructionServices>();
builder.Services.AddScoped<IVisitorManagementRepository, VisitorManagementService>();
builder.Services.AddScoped<IParkingCardRepository, ParkingCardService>();
builder.Services.AddScoped<IBuildingRepository, BuildingService>();
builder.Services.AddScoped<IFeedbackManagementRepository, FeedbackService>();
builder.Services.AddScoped<IServiceTypeRepository, Service_TypeService>();
builder.Services.AddScoped<IPostManagermentRepository, PostManagementService>();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
    options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CORSPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed((host) => true));
});
builder.Services.AddDbContext<abmsContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("value"),
        new MySqlServerVersion(new Version(11, 3, 2)),
        builder => builder.EnableRetryOnFailure()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
        (builder.Configuration["JWT:SecretKey"]))
    };
});
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authoriztion header using the Bearer scheme"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
        new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("CORSPolicy");
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();

app.MapControllers();

app.Run();