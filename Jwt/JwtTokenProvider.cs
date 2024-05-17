using Microsoft.IdentityModel.Tokens;
using MinimalApiTutorial.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApiTutorial.Jwt
{
    public class JwtTokenProvider : IJwtTokenProivder
    {
        private readonly IJwtOptions _opt;

        public JwtTokenProvider(IJwtOptions opt)
        {
            _opt = opt;
        }

        public string GetToken(UserVo userInfo)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));
            SigningCredentials signiture = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                _opt.Issuer,
                _opt.Audience,
                new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Sub,userInfo.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name,userInfo.Name!)
                },
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signiture
                ); ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
