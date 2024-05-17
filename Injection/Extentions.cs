using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MinimalApiTutorial.IRepository;
using MinimalApiTutorial.IService;
using MinimalApiTutorial.Jwt;
using MinimalApiTutorial.Mapper;
using MinimalApiTutorial.Repository;
using MinimalApiTutorial.Service;

namespace MinimalApiTutorial.Extensions
{
    public static class Extentions
    {
        public static void AddServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(UserMapperProfile));
            builder.Services.AddSingleton<IJwtOptions, JwtOptions>();
            builder.Services.AddScoped<IJwtTokenProivder, JwtTokenProvider>();
            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddCarter();

        }
    }
}
