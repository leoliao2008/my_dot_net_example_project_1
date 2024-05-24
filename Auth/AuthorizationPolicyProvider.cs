using Carter.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MinimalApiTutorial.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            if (policyName.Equals("self-only", StringComparison.OrdinalIgnoreCase))
            {
                var builder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                if (int.TryParse(_contextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub), out var userId))
                {
                    string? body = _contextAccessor.HttpContext?.Request.Body.AsStringAsync().Result;
                    UserVo? vo = JsonSerializer.Deserialize<UserVo>(body??string.Empty);
                    if (vo != null)
                    {
                        builder.AddRequirements(new SelfOnlyRequirement(userId, vo.Id));
                        return Task.FromResult(builder.Build()??null);
                    }
                }

            }
            return _defaultProvider.GetPolicyAsync(policyName);
        }
    }
}
