using MinimalApiTutorial.IService;
using MinimalApiTutorial.Model;
using MinimalApiTutorial.Service;

namespace MinimalApiTutorial.Extensions
{
    public static class Extensions
    {
        public static void AddServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
        }

        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => { return TypedResults.Ok("Demo Server Launches Successfully!"); });

            var grp = app.MapGroup("user");

            grp.MapPost("/register", async (UserVo user, IUserService service) => { await service.registerUser(user); });

            grp.MapPost("/login", async (UserVo user, IUserService service) =>
            {
                return await service.login(user.Name!, user.Password!);
            });

            grp.MapPost("/update", async (UserVo user, IUserService service) =>
            {
                return await service.Update(user);
            });
        }
    }
}
