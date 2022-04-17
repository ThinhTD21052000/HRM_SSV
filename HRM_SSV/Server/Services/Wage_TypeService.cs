using AutoMapper;
using Domain.Modals.WageType;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IWage_TypeService
    {
        Task Create(Wage_TypeToAdd wage_TypeToAdd);
        Task Update(Wage_TypeToUpdate wage_TypeToUpdate);
        Task Delete(int id);
        Task<Wage_TypeToGet> Get(int id);
        Task<List<Wage_TypeToGet>> GetList();
    }
    public class Wage_TypeService : IWage_TypeService
    {
        private readonly IWage_TypeRepository _wage_TypeRepository;
        private readonly IMapper _mapper;
        public Wage_TypeService(IWage_TypeRepository wage_TypeRepository, IMapper mapper)
        {
            _wage_TypeRepository = wage_TypeRepository;
            _mapper = mapper;
        }

        public async Task Create(Wage_TypeToAdd wage_TypeToAdd)
        {
            await _wage_TypeRepository.Add(_mapper.Map<Wage_Type>(wage_TypeToAdd));
        }

        public async Task Delete(int id)
        {
            await _wage_TypeRepository.Delete(_mapper.Map<Wage_Type>( await Get(id)));
        }

        public async Task<Wage_TypeToGet> Get(int id)
        {
            return _mapper.Map<Wage_TypeToGet>(await _wage_TypeRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<Wage_TypeToGet>> GetList()
        {
            return _mapper.Map<List<Wage_TypeToGet>>(await _wage_TypeRepository.GetAll());
        }

        public async Task Update(Wage_TypeToUpdate wage_TypeToUpdate)
        {
            await _wage_TypeRepository.Update(_mapper.Map<Wage_Type>(wage_TypeToUpdate));
        }
    }
}
