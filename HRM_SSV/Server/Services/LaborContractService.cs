using AutoMapper;
using Domain.Modals.LaborContract;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface ILaborContractService
    {
        Task Create(LaborContractToAdd laborContractToAdd);
        Task Update(LaborContractToUpdate laborContractToUpdate);
        Task Delete(int id);
        Task<LaborContractToGet> Get(int id);
        Task<List<LaborContractToGet>> GetList();
    }
    public class LaborContractService : ILaborContractService
    {
        private readonly ILaborContractRepository _laborContractRepository;
        private readonly IMapper _mapper;
        public LaborContractService(ILaborContractRepository laborContractRepository, IMapper mapper)
        {
            _laborContractRepository = laborContractRepository;
            _mapper = mapper;
        }

        public async Task Create(LaborContractToAdd laborContractToAdd)
        {
            LaborContract laborContract = _mapper.Map<LaborContract>(laborContractToAdd);
            await _laborContractRepository.Add(laborContract);
        }

        public async Task Delete(int id)
        {
            await _laborContractRepository.Delete(_mapper.Map<LaborContract>( await Get(id)));
        }

        public async Task<LaborContractToGet> Get(int id)
        {
            return _mapper.Map<LaborContractToGet>(await _laborContractRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<LaborContractToGet>> GetList()
        {
            return _mapper.Map<List<LaborContractToGet>>(await _laborContractRepository.GetAll());
        }

        public async Task Update(LaborContractToUpdate laborContractToUpdate)
        {
            await _laborContractRepository.Update(_mapper.Map<LaborContract>(laborContractToUpdate));
        }
    }
}
