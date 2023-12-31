using FlightDocs.Serivce.GroupApi.Data;

using FlightDocs.Service.GroupApi.Extension;
using FlightDocs.Service.GroupApi.Mapper;
using FlightDocs.Service.GroupApi.Service;
using FlightDocs.Service.GroupApi.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//
builder.Services.AddHttpClient("ApplicationUser", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:AuthApi"]));
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

//

//
builder.Services.AddScoped<IGroupService, GroupService>();

//

builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<ITimeService, TimeService>();

//
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

//
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);


builder.AddAppAuthetication();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
