using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Security.Claims;
using Zip.InstallmentsService;
using Zip.InstallmentsService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(p =>
    p.AddPolicy("corsapp", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

# region Register Scoped services
builder.Services.AddScoped<IPaymentPlanFactory, PaymentPlanFactory>();
#endregion 

#region Auth0 Idendtity/Secruity provider Middleware
var AdlibAuth0_Authority = "https://dev-1coulpjsvu0oevxl.us.auth0.com/";//Marking hard code, can read same from enviroment variables, aws secret manager, appsettings as well.
var AdlibAutho0_Audience = "https://localhost:7051/api/"; //Marking hard code, can read same from enviroment variables, aws secret manager, appsettings as well.
var authorityUri = new Uri(AdlibAuth0_Authority);

// 1. Add Authentication Services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = authorityUri.AbsoluteUri;
    options.Audience = AdlibAutho0_Audience;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Zip Installment Api",
            Version = "v1",
            License = new OpenApiLicense
            {
                Name =
                    "LocalHost ClientID: JQjtOIX787JyQVoEwtGqXyIzeFJ5tzfd" // Use to get login in Auth0
            }
        });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "Open Id" }
                },
                AuthorizationUrl = new Uri(authorityUri, "authorize?audience=" + AdlibAutho0_Audience)
            }
        }
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

# endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
        options.DocExpansion(DocExpansion.None);
        //Set all the methods by controller to be collapsed in the swagger UI
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        options.OAuthClientId("");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("corsapp");
app.MapControllers();

app.Run();
