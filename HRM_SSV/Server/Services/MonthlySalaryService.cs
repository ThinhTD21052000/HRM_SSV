using AutoMapper;
using Domain.Modals.MonthlySalary;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IMonthlySalaryService
    {
        Task Create(MonthlySalaryToAdd monthlySalaryToAdd);
        Task Update(MonthlySalaryToUpdate monthlySalaryToUpdate);
        Task Delete(int id);
        Task<MonthlySalaryToGet> Get(int id);
        Task<List<MonthlySalaryToGet>> GetList();
    }
    public class MonthlySalaryService : IMonthlySalaryService
    {
        private readonly IMonthlySalaryRepository _monthlySalaryRepository;
        private readonly IMapper _mapper;
        public MonthlySalaryService(IMonthlySalaryRepository monthlySalaryRepository, IMapper mapper)
        {
            _monthlySalaryRepository = monthlySalaryRepository;
            _mapper = mapper;
        }

        public async Task Create(MonthlySalaryToAdd monthlySalaryToAdd)
        {
            await _monthlySalaryRepository.Add(_mapper.Map<MonthlySalary>(monthlySalaryToAdd));
        }

        public async Task Delete(int id)
        {
            await _monthlySalaryRepository.Delete(_mapper.Map<MonthlySalary>( await Get(id)));
        }

        public async Task<MonthlySalaryToGet> Get(int id)
        {
            return _mapper.Map<MonthlySalaryToGet>(await _monthlySalaryRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<MonthlySalaryToGet>> GetList()
        {
            return _mapper.Map<List<MonthlySalaryToGet>>(await _monthlySalaryRepository.GetAll());
        }

        public async Task Update(MonthlySalaryToUpdate monthlySalaryToUpdate)
        {
            await _monthlySalaryRepository.Update(_mapper.Map<MonthlySalary>(monthlySalaryToUpdate));
        }
    }
}
