using Microsoft.IdentityModel.Tokens;
using MinimalApiTutorial.IService;
using MinimalApiTutorial.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApiTutorial.Service
{
    public class JwtTokenProvider : IJwtTokenProivder
    {
        public string GetToken(UserVo userInfo)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my secret key"));
            SigningCredentials cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                "issuer",
                "audience",
                new Claim[] { 
                    
                },
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                cred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
