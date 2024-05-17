
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MinimalApiTutorial.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtBearerOptions>();
var app = builder.Build();
app.MapGet("/", () => { return TypedResults.Ok("Demo Server Launches Successfully!"); });
app.MapCarter();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
