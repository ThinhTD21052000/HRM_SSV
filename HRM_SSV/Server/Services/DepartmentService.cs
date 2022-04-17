using AutoMapper;
using Domain.Modals.Department;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IDepartmentService
    {
        Task Create(DepartmentToAdd departmentToAdd);
        Task Update(DepartmentToUpdate departmentToUpdate);
        Task Delete(int id);
        Task<DepartmentToGet> Get(int id);
        Task<List<DepartmentToGet>> GetList();
    }
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task Create(DepartmentToAdd departmentToAdd)
        {
            await _departmentRepository.Add(_mapper.Map<Department>(departmentToAdd));
        }

        public async Task Delete(int id)
        {
            await _departmentRepository.Delete(_mapper.Map<Department>( await Get(id)));
        }

        public async Task<DepartmentToGet> Get(int id)
        {
            return _mapper.Map<DepartmentToGet>(await _departmentRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<DepartmentToGet>> GetList()
        {
            return _mapper.Map<List<DepartmentToGet>>(await _departmentRepository.GetAll());
        }

        public async Task Update(DepartmentToUpdate departmentToUpdate)
        {
            await _departmentRepository.Update(_mapper.Map<Department>(departmentToUpdate));
        }
    }
}
