
using MinimalApiTutorial.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServices();
var app = builder.Build();
app.MapEndpoints();

app.Run();
