using AutoMapper;
using Domain.Modals.User;
using Microsoft.AspNetCore.Identity;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IUserService
    {
        Task<IdentityResult> Create(UserToAdd userToAdd);
        Task<IdentityResult> Update(UserToUpdate userToUpdate);
        Task<IdentityResult> Delete(int id);
        Task<UserToGet> Get(int id);
        Task<List<UserToGet>> GetList();
    }
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(UserManager<User> userManager, IMapper mapper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Create(UserToAdd userToAdd)
        {
            User user = _mapper.Map<User>(userToAdd);
            user.Id = Guid.NewGuid();
            user.SecurityStamp = user.Id.ToString();
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> Delete(int id)
        {
            return await _userManager.DeleteAsync(_mapper.Map<User>( await Get(id)));
        }

        public async Task<UserToGet> Get(int id)
        {
            return _mapper.Map<UserToGet>(await _userRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<UserToGet>> GetList()
        {
            return _mapper.Map<List<UserToGet>>(await _userRepository.GetAll());
        }

        public async Task<IdentityResult> Update(UserToUpdate userToUpdate)
        {
            return await _userManager.UpdateAsync(_mapper.Map<User>(userToUpdate));
        }
    }
}
