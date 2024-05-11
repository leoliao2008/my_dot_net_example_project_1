using ControllerApiTutorial.Models;
using MinimalApiTutorial.Common;
using MinimalApiTutorial.IService;
using MinimalApiTutorial.Model;
using MinimalApiTutorial.Repository;

namespace MinimalApiTutorial.Service
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepo;
        public UserService(IConfiguration config)
        {
            string conString = config.GetConnectionString("Develope")!;
            _userRepo = new UserRepository(conString);
        }

        public async Task<UserVo> login(string user_name, string pw)
        {
            pw = CrytographyUtils.HashPassword(pw);
            var entity = await _userRepo.Login(user_name, pw);
            UserVo vo = new UserVo();
            vo.Name = entity.User_Name;
            vo.NickName = entity.Nick_name;
            vo.Email = entity.Email;
            vo.Gender = entity.Gender;
            vo.Cellphone = entity.Phone;
            vo.CoupleId = entity.CoupleId;
            vo.Id = entity.Id;
            vo.Token = "ssssssssssssssssss";
            return vo;

        }

        public async Task<int> registerUser(UserVo user)
        {
            UserEntity entity = new UserEntity();
            entity.User_Name = user.Name;
            entity.Email = user.Email;
            entity.Nick_name = user.NickName;
            entity.Phone = user.Cellphone;
            entity.CoupleId = user.CoupleId;
            entity.Gender = user.Gender;
            entity.Pwd = CrytographyUtils.HashPassword(user.Password!);
            return await _userRepo.Register(entity);
        }

        public async Task<bool> Update(UserVo user)
        {
            UserEntity entity = new UserEntity();
            entity.Id = user.Id;
            entity.User_Name = user.Name;
            entity.Email = user.Email;
            entity.Nick_name = user.NickName;
            entity.Phone = user.Cellphone;
            entity.CoupleId = user.CoupleId;
            entity.Gender = user.Gender;
            entity.Pwd = CrytographyUtils.HashPassword(user.Password!);
            return await _userRepo.Update(entity);
        }
    }
}
