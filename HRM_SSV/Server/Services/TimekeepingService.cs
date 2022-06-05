using AutoMapper;
using Domain.Modals.Timekeeping;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface ITimekeepingService
    {
        Task Create(TimekeepingToAdd timekeepingToAdd);
        Task Update(TimekeepingToUpdate timekeepingToUpdate);
        Task Delete(int id);
        Task<TimekeepingToGet> Get(int id);
        Task<List<TimekeepingToGet>> GetList();
    }
    public class TimekeepingService : ITimekeepingService
    {
        private readonly ITimekeepingRepository _timekeepingRepository;
        private readonly IMapper _mapper;
        public TimekeepingService(ITimekeepingRepository timekeepingRepository, IMapper mapper)
        {
            _timekeepingRepository = timekeepingRepository;
            _mapper = mapper;
        }

        public async Task Create(TimekeepingToAdd timekeepingToAdd)
        {
            await _timekeepingRepository.Add(_mapper.Map<Timekeeping>(timekeepingToAdd));
        }

        public async Task Delete(int id)
        {
            await _timekeepingRepository.Delete(await _timekeepingRepository.Get(x => x.Id == id));
        }

        public async Task<TimekeepingToGet> Get(int id)
        {
            return _mapper.Map<TimekeepingToGet>(await _timekeepingRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<TimekeepingToGet>> GetList()
        {
            return _mapper.Map<List<TimekeepingToGet>>(await _timekeepingRepository.GetAll());
        }

        public async Task Update(TimekeepingToUpdate timekeepingToUpdate)
        {
            await _timekeepingRepository.Update(_mapper.Map<Timekeeping>(timekeepingToUpdate));
        }
    }
}
