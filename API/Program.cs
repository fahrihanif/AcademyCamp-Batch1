using System.Text;
using API.Data;
using API.DTOs.Responses;
using API.Repositories.Data;
using API.Repositories.Interfaces;
using API.Services.Data;
using API.Services.Interfaces;
using API.Utilities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TokenHandler = API.Utilities.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
       .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errorFields = context.ModelState.ToDictionary(
                                                                  kvp => kvp.Key,
                                                                  kvp => kvp.Value?
                                                                            .Errors
                                                                            .Select(e => e.ErrorMessage)
                                                                            .ToArray()
                                                                 );
                var errorResponse = new ErrorResponseDto(StatusCodes.Status400BadRequest,
                                                         "Bad request. One or more validation errors occurred.",
                                                         errorFields);
                return new BadRequestObjectResult(errorResponse);
            };
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("EmployeeConnection");
builder.Services.AddDbContext<EmployeeDbContext>(option =>
                                                     option.UseSqlServer(connectionString,
                                                                         cfg => cfg.EnableRetryOnFailure()));

// Repository Configuration
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();

// Service Configuration
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// FluentValidation
builder.Services.AddFluentValidationAutoValidation()
       .AddValidatorsFromAssembly(typeof(Program).Assembly);

// Utilities Configuration
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
var jwtKey = builder.Configuration["JWTService:Key"];
var jwtIssuer = builder.Configuration["JWTService:Issuer"];
var jwtAudience = builder.Configuration["JWTService:Audience"];
builder.Services.AddTransient<ITokenHandler, TokenHandler>(_ => new TokenHandler(jwtKey, jwtIssuer, jwtAudience,
                                                            Convert.ToInt32(builder.Configuration
                                                                                ["JWTService:DurationInMinute"])));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new() {
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<EmployeeDbContext>();
if (db != null) await db.Database.MigrateAsync();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI();

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
