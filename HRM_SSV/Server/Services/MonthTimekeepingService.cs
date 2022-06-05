using AutoMapper;
using Domain.Modals.MonthTimekeeping;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IMonthTimekeepingService
    {
        Task Create(MonthTimekeepingToAdd MonthTimekeepingToAdd);
        Task Update(MonthTimekeepingToUpdate MonthTimekeepingToUpdate);
        Task Delete(int id);
        Task<MonthTimekeepingToGet> Get(int id);
        Task<List<MonthTimekeepingToGet>> GetList();
    }
    public class MonthTimekeepingService : IMonthTimekeepingService
    {
        private readonly IMonthTimekeepingRepository _MonthTimekeepingRepository;
        private readonly IMapper _mapper;
        public MonthTimekeepingService(IMonthTimekeepingRepository MonthTimekeepingRepository, IMapper mapper)
        {
            _MonthTimekeepingRepository = MonthTimekeepingRepository;
            _mapper = mapper;
        }

        public async Task Create(MonthTimekeepingToAdd MonthTimekeepingToAdd)
        {
            await _MonthTimekeepingRepository.Add(_mapper.Map<MonthTimeKeeping>(MonthTimekeepingToAdd));
        }

        public async Task Delete(int id)
        {
            await _MonthTimekeepingRepository.Delete(await _MonthTimekeepingRepository.Get(x => x.Id == id));
        }

        public async Task<MonthTimekeepingToGet> Get(int id)
        {
            return _mapper.Map<MonthTimekeepingToGet>(await _MonthTimekeepingRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<MonthTimekeepingToGet>> GetList()
        {
            return _mapper.Map<List<MonthTimekeepingToGet>>(await _MonthTimekeepingRepository.GetAll());
        }

        public async Task Update(MonthTimekeepingToUpdate MonthTimekeepingToUpdate)
        {
            await _MonthTimekeepingRepository.Update(_mapper.Map<MonthTimeKeeping>(MonthTimekeepingToUpdate));
        }
    }
}
