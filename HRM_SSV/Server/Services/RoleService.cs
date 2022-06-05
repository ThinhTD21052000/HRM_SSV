using AutoMapper;
using Domain.Modals.Role;
using Microsoft.AspNetCore.Identity;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IRoleService
    {
        Task Create(RoleToAdd roleToAdd);
        Task Update(RoleToUpdate RoleToUpdate);
        Task Delete(Guid id);
        Task<RoleToGet> Get(Guid id);
        Task<List<RoleToGet>> GetList();
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper, RoleManager<Role> roleManager)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task Create(RoleToAdd roleToAdd)
        {
            if (!await _roleManager.RoleExistsAsync(roleToAdd.Name))
            {
                roleToAdd.Id = Guid.NewGuid();
                await _roleManager.CreateAsync(_mapper.Map<Role>(roleToAdd));
            }
        }

        public async Task Delete(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
        }

        public async Task<RoleToGet> Get(Guid id)
        {
            return _mapper.Map<RoleToGet>(await _roleManager.FindByIdAsync(id.ToString()));
        }

        public async Task<List<RoleToGet>> GetList()
        {
            return _mapper.Map<List<RoleToGet>>(await _roleRepository.GetAll());
        }

        public async Task Update(RoleToUpdate roleToUpdate)
        {
            await _roleManager.UpdateAsync(_mapper.Map<Role>(roleToUpdate));
        }
    }
}
