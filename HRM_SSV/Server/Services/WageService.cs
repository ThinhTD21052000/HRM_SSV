using AutoMapper;
using Domain.Modals.Wage;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IWageService
    {
        Task Create(WageToAdd wageToAdd);
        Task Update(WageToUpdate wageToUpdate);
        Task Delete(int id);
        Task<WageToGet> Get(int id);
        Task<List<WageToGet>> GetList();
    }
    public class WageService : IWageService
    {
        private readonly IWageRepository _wageRepository;
        private readonly IMapper _mapper;
        public WageService(IWageRepository wageRepository, IMapper mapper)
        {
            _wageRepository = wageRepository;
            _mapper = mapper;
        }

        public async Task Create(WageToAdd wageToAdd)
        {
            await _wageRepository.Add(_mapper.Map<Wage>(wageToAdd));
        }

        public async Task Delete(int id)
        {
            await _wageRepository.Delete(_mapper.Map<Wage>( await Get(id)));
        }

        public async Task<WageToGet> Get(int id)
        {
            return _mapper.Map<WageToGet>(await _wageRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<WageToGet>> GetList()
        {
            return _mapper.Map<List<WageToGet>>(await _wageRepository.GetAll());
        }

        public async Task Update(WageToUpdate wageToUpdate)
        {
            await _wageRepository.Update(_mapper.Map<Wage>(wageToUpdate));
        }
    }
}
