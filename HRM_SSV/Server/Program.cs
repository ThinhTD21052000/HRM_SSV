using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

var json = new WebClient().DownloadString("https://cognito-idp.ap-northeast-1.amazonaws.com/ap-northeast-1_YoOv7XErX/.well-known/jwks.json");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "https://cognito-idp.ap-northeast-1.amazonaws.com/ap-northeast-1_YoOv7XErX",
        IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
        {
            // get JsonWebKeySet from AWS

            // serialize the result
            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
            // cast the result to be the type expected by IssuerSigningKeyResolver
            return keys;
        },
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
    };
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
