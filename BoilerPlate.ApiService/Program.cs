using BoilerPlate.ApiService.Swagger;
using BoilerPlate.Application;
using BoilerPlate.Application.Integrations;
using BoilerPlate.Auth;
using BoilerPlate.Domain.Entities.Settings;
using BoilerPlate.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLocalizationLayer();
builder.Services.AddApplicationLayer();
builder.Services.AddIntegrationLayer();
builder.Services.AddAuth0Layer();
// Database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnectionString"));
});

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add localization services
builder.Services.AddLocalization();

// Add services to the container.
builder.Services.AddProblemDetails();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Add Microsoft Entra Auth
//builder.Services.AddMicrosoftEntraAuth(builder.Configuration);

//Add Auth0 Auth
builder.Services.AddAuth0Auth(builder.Configuration);
builder.Services.AddAuth0Scopes(new string[] { "read:woocommerce:product" });

//Settings
builder.Services.ConfigureAppSettings(builder.Configuration);

builder.Host.ConfigureSerilog(builder.Configuration);
//builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// Configure localization
app.UseLocalization();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseConfiguredSwagger(provider);

// Add middleware to validate the JWT tokens issued by Azure AD.
//app.UseMicrosoftEntraValidation(builder.Configuration);

// Add middleware to validate the audience and issuer.
app.UseAuthentication();
app.UseAuthorization();

// Map swagger to v1
app.MapDefaultSwagger(provider, "v1");

app.MapControllers();
app.Run();
