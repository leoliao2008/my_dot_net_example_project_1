using MinimalApiTutorial.Model;

namespace MinimalApiTutorial.Jwt
{
    public interface IJwtTokenProivder
    {
        string GetToken(UserVo userInfo);
    }
}
