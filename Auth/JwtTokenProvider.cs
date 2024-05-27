using Microsoft.IdentityModel.Tokens;
using MinimalApiTutorial.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MinimalApiTutorial.Jwt
{
    public class JwtTokenProvider : IJwtTokenProivder
    {
        private readonly JwtOptions _opt;
        public static readonly string SECTION_NAME_FOR_JWT_TOKEN = "JWT";

        public JwtTokenProvider(IConfiguration config)
        {
            _opt = config.GetSection(SECTION_NAME_FOR_JWT_TOKEN).Get<JwtOptions>()!;
        }

        public string GetToken(UserVo userInfo)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));
            SigningCredentials signiture = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                _opt.Issuer,
                _opt.Audience,
                new Claim[] {
                    new Claim(ClaimTypes.Sid,userInfo.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name,userInfo.Name!)
                },
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(_opt.Expire),
                signiture
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
