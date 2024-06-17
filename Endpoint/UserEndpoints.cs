using Carter;
using MinimalApiTutorial.IService;
using MinimalApiTutorial.Model;

namespace MinimalApiTutorial.Endpoint
{
    public class UserEndpoints : CarterModule
    {

        public UserEndpoints() : base("/user") { }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPost("/register", async (UserVo user, IUserService service) => { await service.registerUser(user); });

            app.MapPost("/login", async (HttpContext ctx, UserVo user, IUserService service) =>
            {
                UserVo vo = await service.login(user.Name!, user.Password!);
                return TypedResults.Ok(vo);

            });


            app.MapPost("/update", async (UserVo user, IUserService service) =>
            {
                return await service.Update(user);

            }).RequireAuthorization("self-only");


        }
    }
}
