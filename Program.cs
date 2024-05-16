
using Carter;
using MinimalApiTutorial.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();
var app = builder.Build();
app.MapGet("/", () => { return TypedResults.Ok("Demo Server Launches Successfully!"); });
app.MapCarter();

app.Run();
