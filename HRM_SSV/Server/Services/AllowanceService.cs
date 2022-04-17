using AutoMapper;
using Domain.Modals.Allowance;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IAllowanceService
    {
        Task Create(AllowanceToAdd allowanceToAdd);
        Task Update(AllowanceToUpdate allowanceToUpdate);
        Task Delete(int id);
        Task<AllowanceToGet> Get(int id);
        Task<List<AllowanceToGet>> GetList();
    }
    public class AllowanceService : IAllowanceService
    {
        private readonly IAllowanceRepository _allowanceRepository;
        private readonly IMapper _mapper;
        public AllowanceService(IAllowanceRepository allowanceRepository, IMapper mapper)
        {
            _allowanceRepository = allowanceRepository;
            _mapper = mapper;
        }

        public async Task Create(AllowanceToAdd allowanceToAdd)
        {
            await _allowanceRepository.Add(_mapper.Map<Allowance>(allowanceToAdd));
        }

        public async Task Delete(int id)
        {
            await _allowanceRepository.Delete(_mapper.Map<Allowance>( await Get(id)));
        }

        public async Task<AllowanceToGet> Get(int id)
        {
            return _mapper.Map<AllowanceToGet>(await _allowanceRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<AllowanceToGet>> GetList()
        {
            return _mapper.Map<List<AllowanceToGet>>(await _allowanceRepository.GetAll());
        }

        public async Task Update(AllowanceToUpdate allowanceToUpdate)
        {
            await _allowanceRepository.Update(_mapper.Map<Allowance>(allowanceToUpdate));
        }
    }
}
