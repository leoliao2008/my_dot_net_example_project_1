
using Carter;
using MinimalApiTutorial.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServices();
builder.SetupJWTAuthentication();
builder.SetupGlobalExceptionHandler();
var app = builder.Build();
app.MapGet("/", () => { return TypedResults.Ok("Demo Server Launches Successfully!"); });
app.MapCarter();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();

app.Run();
