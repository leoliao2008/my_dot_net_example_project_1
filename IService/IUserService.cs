using MinimalApiTutorial.Model;

namespace MinimalApiTutorial.IService
{
    public interface IUserService
    {
        public Task<int> registerUser(UserVo user);
        public Task<UserVo> login(string user_name, string pw);
        public Task<bool> Update(UserVo user);
    }
}
