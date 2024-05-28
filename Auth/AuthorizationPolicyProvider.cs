using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MinimalApiTutorial.Model;
using System.Security.Claims;
using System.Text.Json;

namespace MinimalApiTutorial.Auth
{
    public class AuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly ILogger<AuthorizationPolicyProvider> _logger;
        private readonly DefaultAuthorizationPolicyProvider _defaultProvider;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthorizationPolicyProvider(ILogger<AuthorizationPolicyProvider> logger, IOptions<AuthorizationOptions> opt, IHttpContextAccessor ctxAccessor)
        {
            _logger = logger;
            _defaultProvider = new DefaultAuthorizationPolicyProvider(opt);
            _contextAccessor = ctxAccessor;

        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return _defaultProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return _defaultProvider.GetFallbackPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName == "self-only")
            {
                var builder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                var context = _contextAccessor.HttpContext;
                if (context != null && int.TryParse(context.User.FindFirstValue(ClaimTypes.Sid), out var userId))
                {
                    //Note(IMPORTANT!): Enable the buffering first before requesting data
                    context.Request.EnableBuffering();
                    Stream rqBody = context.Request.Body;
                    rqBody.Seek(0, SeekOrigin.Begin);
                    StreamReader reader = new StreamReader(rqBody);
                    string jsString = reader.ReadToEndAsync().Result;
                    rqBody.Seek(0, SeekOrigin.Begin);
                    UserVo? vo = JsonSerializer.Deserialize<UserVo>(jsString);
                    if (vo != null)
                    {
                        builder.AddRequirements(new SelfOnlyRequirement(userId, vo.Id));
                        return Task.FromResult(builder.Build() ?? null);
                    }
                }

            }
            return _defaultProvider.GetPolicyAsync(policyName);
        }
    }
}
