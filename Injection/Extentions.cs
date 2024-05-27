using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MinimalApiTutorial.Auth;
using MinimalApiTutorial.IRepository;
using MinimalApiTutorial.IService;
using MinimalApiTutorial.Jwt;
using MinimalApiTutorial.Mapper;
using MinimalApiTutorial.Repository;
using MinimalApiTutorial.Service;
using System.Security.Claims;
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
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCarter();
        }

        public static void SetupJWTAuthentication(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
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
            
            builder.Services.AddSingleton<IAuthorizationPolicyProvider,AuthorizationPolicyProvider>();
            builder.Services.AddSingleton<IAuthorizationHandler,SelfOnlyPolicyHandler>();
            builder.Services.AddAuthorization();


        }
    }
}
