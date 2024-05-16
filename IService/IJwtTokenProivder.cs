using MinimalApiTutorial.Model;

namespace MinimalApiTutorial.IService
{
    public interface IJwtTokenProivder
    {
        string GetToken(UserVo userInfo);
    }
}
