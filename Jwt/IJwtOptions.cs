namespace MinimalApiTutorial.Jwt
{
    public interface IJwtOptions
    {
        string Issuer { get; }
        string Audience { get; }
        string SecretKey { get; }
    }
}
