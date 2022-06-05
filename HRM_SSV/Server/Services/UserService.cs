using AutoMapper;
using Domain.Modals.User;
using Microsoft.AspNetCore.Identity;
using Server.Entities;
using Server.Repositories;
using System.Text;
using System.Security.Cryptography;

namespace Server.Services
{
    public interface IUserService
    {
        Task<IdentityResult> Create(UserToAdd userToAdd);
        Task<IdentityResult> Update(UserToUpdate userToUpdate);
        Task<IdentityResult> Delete(Guid id);
        Task<UserToGet> Get(Guid id);
        Task<List<UserToGet>> GetList();
        Task<IdentityResult> ChangePassword(UserToGet user, string oldPassword, string newPassword);
        Task<bool> CheckPassword(Guid id, string Password);
    }
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ITeamRepository _teamRepository;
        public UserService(UserManager<User> userManager, IMapper mapper, IUserRepository userRepository,
            IPositionRepository positionRepository, ITeamRepository teamRepository, SignInManager<User> signinManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
            _positionRepository = positionRepository;
            _teamRepository = teamRepository;
            _signinManager = signinManager;
        }

        public async Task<IdentityResult> Create(UserToAdd userToAdd)
        {
            User user = _mapper.Map<User>(userToAdd);
            user.Id = Guid.NewGuid();
            user.SecurityStamp = user.Id.ToString();
            var iden = await _userManager.CreateAsync(user, userToAdd.Password);
            if (iden.Succeeded)
            {
                var userLogin = await _userManager.FindByNameAsync(user.UserName);
                if (userLogin != null && await _userManager.CheckPasswordAsync(user, userToAdd.Password))
                    await _userManager.AddToRoleAsync(user, userToAdd.RoleName);
            }
            return iden;
        }
        public async Task<IdentityResult> Delete(Guid id)
        {
            return await _userManager.DeleteAsync(_mapper.Map<User>( await Get(id)));
        }

        public async Task<UserToGet> Get(Guid id)
        {
            var item = _mapper.Map<UserToGet>(await _userManager.FindByIdAsync(id.ToString()));
            item.ImageFile = Convert.ToBase64String(item.Avatar);
            var roles = _userManager.GetRolesAsync(_mapper.Map<User>(item));
            item.RoleName = roles.Result[0];
            var team = _teamRepository.GetBasic(x => x.Id == item.TeamId);
            item.TeamName = team.Name;
            var position = _positionRepository.GetBasic(x => x.Id == item.PositionId);
            item.PositionName = position.Name;
            return item;
        }

        public static string Decrypt(string sData)
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }

        public async Task<List<UserToGet>> GetList()
        {
            var list = _mapper.Map<List<UserToGet>>(await _userRepository.GetAll());
            foreach (var item in list)
            {
                item.ImageFile = Convert.ToBase64String(item.Avatar);
                var roles = _userManager.GetRolesAsync(_mapper.Map<User>(item));
                item.RoleName = roles.Result[0];
                var team = _teamRepository.GetBasic(x => x.Id == item.TeamId);
                item.TeamName = team.Name;
                var position = _positionRepository.GetBasic(x => x.Id == item.PositionId);
                item.PositionName = position.Name;
                item.FullName = $"{item.LastName} {item.FirstName}";
            }
            return list;
        }

        public async Task<IdentityResult> Update(UserToUpdate userToUpdate)
        {
            var user = await _userManager.FindByIdAsync(userToUpdate.Id.ToString());
            user.UserName = userToUpdate.UserName;
            user.FirstName = userToUpdate.FirstName;
            user.LastName = userToUpdate.LastName;
            user.Sex = userToUpdate.Sex;
            user.Email = userToUpdate.Email;
            user.PhoneNumber = userToUpdate.PhoneNumber;
            user.Address = userToUpdate.Address;
            user.DoB = userToUpdate.DoB;
            user.PositionId = userToUpdate.PositionId;
            user.TeamId = userToUpdate.TeamId;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(UserToGet user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(_mapper.Map<User>(user), oldPassword, newPassword);
        }

        public async Task<bool> CheckPassword(Guid id, string Password)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return await _userManager.CheckPasswordAsync(user, Password);
        }
    }
}
