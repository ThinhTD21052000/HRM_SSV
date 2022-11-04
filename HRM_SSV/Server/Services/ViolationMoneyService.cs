using AutoMapper;
using Domain.Modals.ViolationMoney;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IViolationMoneyService
    {
        Task Create(ViolationMoneyToAdd violationMoneyToAdd);
        Task Update(ViolationMoneyToUpdate violationMoneyToUpdate);
        Task Delete(int id);
        Task<ViolationMoneyToGet> Get(int id);
        Task<List<ViolationMoneyToGet>> GetList();
    }
    public class ViolationMoneyService : IViolationMoneyService
    {
        private readonly IViolationMoneyRepository _violationMoneyRepository;
        private readonly IMapper _mapper;
        public ViolationMoneyService(IViolationMoneyRepository ViolationMoneyRepository, IMapper mapper)
        {
            _violationMoneyRepository = ViolationMoneyRepository;
            _mapper = mapper;
        }

        public async Task Create(ViolationMoneyToAdd ViolationMoneyToAdd)
        {
            await _violationMoneyRepository.Add(_mapper.Map<ViolationMoney>(ViolationMoneyToAdd));
        }

        public async Task Delete(int id)
        {
            await _violationMoneyRepository.Delete(_mapper.Map<ViolationMoney>( await Get(id)));
        }

        public async Task<ViolationMoneyToGet> Get(int id)
        {
            return _mapper.Map<ViolationMoneyToGet>(await _violationMoneyRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<ViolationMoneyToGet>> GetList()
        {
            return _mapper.Map<List<ViolationMoneyToGet>>(await _violationMoneyRepository.GetAll());
        }

        public async Task Update(ViolationMoneyToUpdate violationMoneyToUpdate)
        {
            await _violationMoneyRepository.Update(_mapper.Map<ViolationMoney>(violationMoneyToUpdate));
        }
    }
}
