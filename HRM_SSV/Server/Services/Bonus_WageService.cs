using AutoMapper;
using Domain.Modals.Bonus_Wage;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IBonus_WageService
    {
        Task Create(Bonus_WageToAdd bonus_WageToAdd);
        Task Update(Bonus_WageToUpdate bonus_WageToUpdate);
        Task Delete(int id);
        Task<Bonus_WageToGet> Get(int id);
        Task<List<Bonus_WageToGet>> GetList();
    }
    public class Bonus_WageService : IBonus_WageService
    {
        private readonly IBonus_WageRepository _bonus_WageRepository;
        private readonly IMapper _mapper;
        public Bonus_WageService(IBonus_WageRepository bonus_WageRepository, IMapper mapper)
        {
            _bonus_WageRepository = bonus_WageRepository;
            _mapper = mapper;
        }

        public async Task Create(Bonus_WageToAdd bonus_WageToAdd)
        {
            await _bonus_WageRepository.Add(_mapper.Map<Bonus_Wage>(bonus_WageToAdd));
        }

        public async Task Delete(int id)
        {
            await _bonus_WageRepository.Delete(_mapper.Map<Bonus_Wage>( await Get(id)));
        }

        public async Task<Bonus_WageToGet> Get(int id)
        {
            return _mapper.Map<Bonus_WageToGet>(await _bonus_WageRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<Bonus_WageToGet>> GetList()
        {
            return _mapper.Map<List<Bonus_WageToGet>>(await _bonus_WageRepository.GetAll());
        }

        public async Task Update(Bonus_WageToUpdate bonus_WageToUpdate)
        {
            await _bonus_WageRepository.Update(_mapper.Map<Bonus_Wage>(bonus_WageToUpdate));
        }
    }
}
