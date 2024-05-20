using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MinimalApiTutorial.IRepository;
using MinimalApiTutorial.IService;
using MinimalApiTutorial.Jwt;
using MinimalApiTutorial.Mapper;
using MinimalApiTutorial.Repository;
using MinimalApiTutorial.Service;
using System.Text;

namespace MinimalApiTutorial.Extensions
{
    public static class Extentions
    {
        public static void AddServices(this IHostApplicationBuilder builder)
        {

            builder.Services.AddAutoMapper(typeof(UserMapperProfile));
            builder.Services.AddScoped<IJwtTokenProivder, JwtTokenProvider>();
            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddCarter();
        }

        public static void SetupJWTAuthentication(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                var jwtSetupOpt = builder.Configuration.GetSection(JwtTokenProvider.SECTION_NAME_FOR_JWT_TOKEN).Get<JwtOptions>()!;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidIssuer = jwtSetupOpt.Issuer,
                   ValidAudience = jwtSetupOpt.Audience,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetupOpt.SecretKey))
                };
            });
           builder.Services.AddAuthorization();
        }
    }
}
