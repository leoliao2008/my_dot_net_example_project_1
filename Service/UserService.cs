using AutoMapper;
using ControllerApiTutorial.Models;
using MinimalApiTutorial.Common;
using MinimalApiTutorial.IRepository;
using MinimalApiTutorial.IService;
using MinimalApiTutorial.Model;
using System.IdentityModel.Tokens.Jwt;

namespace MinimalApiTutorial.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepo = userRepository;
            _mapper = mapper;
        }

        public async Task<UserVo> login(string user_name, string pw)
        {
            pw = CrytographyUtils.HashPassword(pw);
            var entity = await _userRepo.Login(user_name, pw);
            UserVo vo = _mapper.Map<UserVo>(entity);

            return vo;

        }

        public async Task<int> registerUser(UserVo user)
        {
            UserEntity entity = _mapper.Map<UserEntity>(user);
            entity.Pwd = CrytographyUtils.HashPassword(user.Password!);
            return await _userRepo.Register(entity);
        }

        public async Task<bool> Update(UserVo user)
        {
            UserEntity entity = _mapper.Map<UserEntity>(user);
            entity.Pwd = CrytographyUtils.HashPassword(user.Password!);
            return await _userRepo.Update(entity);
        }
    }
}
