using ControllerApiTutorial.Models;

namespace MinimalApiTutorial.IRepository
{
    public interface IUserRepository
    {
        public Task<UserEntity> Login(string userName, string pw);

        public Task<int> Register(UserEntity user);

        public Task<bool> Update(UserEntity user);
    }
}
