using AutoMapper;
using Domain.Modals.Role;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IRoleService
    {
        Task Create(RoleToAdd roleToAdd);
        Task Update(RoleToUpdate RoleToUpdate);
        Task Delete(int id);
        Task<RoleToGet> Get(int id);
        Task<List<RoleToGet>> GetList();
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task Create(RoleToAdd roleToAdd)
        {
            await _roleRepository.Add(_mapper.Map<Role>(roleToAdd));
        }

        public async Task Delete(int id)
        {
            await _roleRepository.Delete(_mapper.Map<Role>( await Get(id)));
        }

        public async Task<RoleToGet> Get(int id)
        {
            return _mapper.Map<RoleToGet>(await _roleRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<RoleToGet>> GetList()
        {
            return _mapper.Map<List<RoleToGet>>(await _roleRepository.GetAll());
        }

        public async Task Update(RoleToUpdate roleToUpdate)
        {
            await _roleRepository.Update(_mapper.Map<Role>(roleToUpdate));
        }
    }
}
