using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace MinimalApiTutorial.Jwt
{
    public class JwtBearerOptionSetup : IOptionsSnapshot<JwtBearerOptions>
    {
        public JwtBearerOptions Value => throw new NotImplementedException();

        public JwtBearerOptions Get(string? name)
        {
            throw new NotImplementedException();
        }
    }
}
