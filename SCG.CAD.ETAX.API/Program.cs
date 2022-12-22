global using Microsoft.EntityFrameworkCore;
global using SCG.CAD.ETAX.DAL.EntityFramework;
global using SCG.CAD.ETAX.DAL;
global using SCG.CAD.ETAX.DAL.CONTROLLER;
global using SCG.CAD.ETAX.DAL.MODEL;
global using SCG.CAD.ETAX.MODEL;
global using Microsoft.AspNetCore.Mvc;
global using SCG.CAD.ETAX.API.Repositories;
global using SCG.CAD.ETAX.API.Services;
global using System.Data;
global using SCG.CAD.ETAX.MODEL.etaxModel;
global using System.Collections;
global using System.Globalization;
global using Newtonsoft.Json;
global using SCG.CAD.ETAX.MODEL.CustomModel;
using Microsoft.OpenApi.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Localization;
using SCG.CAD.ETAX.API.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(options => // <--- Setup logging
{
    // Specify all that you need here:
    options.LoggingFields = HttpLoggingFields.All;
    //options.ResponseHeaders.Add("Non-Sensitive");
    //options.MediaTypeOptions.AddText("application/javascript");
    //options.RequestBodyLogLimit = 4096;
    //options.ResponseBodyLogLimit = 4096;
});

var connectionString = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["ConnectionStr"];

var ci = CultureInfo.GetCultureInfo("en-US");

Thread.CurrentThread.CurrentCulture = ci;
Thread.CurrentThread.CurrentUICulture = ci;
// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>
    (options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 3.1 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
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
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["Issuer"],
        ValidAudience = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["Key"])) //Configuration["JwtToken:SecretKey"]
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US")
});

//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SCG.CAD.ETAX.API");
//    // c.RoutePrefix = "";
//});


//app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseLoggingHandler();

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

app.MapControllers();

// app.UseResponseBuffering();

// app.UseResponseCaching();

app.Run();

