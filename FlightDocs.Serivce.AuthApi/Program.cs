using FlightDocs.Serivce.AuthApi.Data;
using FlightDocs.Serivce.AuthApi.Models;
using FlightDocs.Serivce.AuthApi.Service.IService;
using FlightDocs.Serivce.AuthApi.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FlightDocs.Serivce.AuthApi.Models.Dto;
using FlightDocs.Service.AuthApi.Mapper;
using FlightDocs.Serivce.AuthApi.Models.Dto.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
//Add Email Configs
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();
//
builder.Services.AddScoped<IApplicationUserSerivce, ApplicationUserSerivce>();
//// Validator
builder.Services.AddScoped<IUserValidator, UserValidator>();
builder.Services.AddScoped<RegistrationRequestRule>();
//
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

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
