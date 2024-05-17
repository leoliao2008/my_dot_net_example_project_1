namespace MinimalApiTutorial.Jwt
{
    public class JwtOptions : IJwtOptions
    {
        private readonly IConfiguration _configuration;
        private readonly string JWT_CONFIG_NAME = "Jwt";
        private readonly string JWT_CONFIG_VALUE_NAME_ISSUER = "Issuer";
        private readonly string JWT_CONFIG_VALUE_NAME_AUDIENCE = "Audience";
        private readonly string JWT_CONFIG_VALUE_NAME_SECRET_KEY = "Secret";

        public JwtOptions(IConfiguration configuration) { 
            _configuration = configuration.GetSection(JWT_CONFIG_NAME);
        }


        public string Issuer => _configuration.GetValue<string>(JWT_CONFIG_VALUE_NAME_ISSUER)!;

        public string Audience => _configuration.GetValue<string>(JWT_CONFIG_VALUE_NAME_AUDIENCE)!;

        public string SecretKey => _configuration.GetValue<string>(JWT_CONFIG_VALUE_NAME_SECRET_KEY)!;
    }
}
