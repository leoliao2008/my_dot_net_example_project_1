using Carter;
using JWT.Algorithms;
using JWT.Extensions.AspNetCore;
using MinimalApiTutorial.IRepository;
using MinimalApiTutorial.IService;
using MinimalApiTutorial.Mapper;
using MinimalApiTutorial.Repository;
using MinimalApiTutorial.Service;
using System.Security.Cryptography;

namespace MinimalApiTutorial.Extensions
{
    public static class Extensions
    {
        public static void AddServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(UserMapperProfile));
            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddCarter();
            builder.Services.AddAuthentication(
                opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtAuthenticationDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtAuthenticationDefaults.AuthenticationScheme;
                }
                )
                .AddJwt(opt =>
                {
                    opt.Keys = ["sdfdfdafdafdafadfadfadfadfdf"];
                    opt.VerifySignature = true;

                });
            builder.Services.AddSingleton<IAlgorithmFactory>(new DelegateAlgorithmFactory((ctx) => {
                IJwtAlgorithm alo 
                return alo;
            })); ;
        }
    }
}
