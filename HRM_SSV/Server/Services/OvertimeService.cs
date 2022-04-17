using AutoMapper;
using Domain.Modals.Overtime;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IOvertimeService
    {
        Task Create(OvertimeToAdd overtimeToAdd);
        Task Update(OvertimeToUpdate overtimeToUpdate);
        Task Delete(int id);
        Task<OvertimeToGet> Get(int id);
        Task<List<OvertimeToGet>> GetList();
    }
    public class OvertimeService : IOvertimeService
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;
        public OvertimeService(IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }

        public async Task Create(OvertimeToAdd overtimeToAdd)
        {
            await _overtimeRepository.Add(_mapper.Map<Overtime>(overtimeToAdd));
        }

        public async Task Delete(int id)
        {
            await _overtimeRepository.Delete(_mapper.Map<Overtime>( await Get(id)));
        }

        public async Task<OvertimeToGet> Get(int id)
        {
            return _mapper.Map<OvertimeToGet>(await _overtimeRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<OvertimeToGet>> GetList()
        {
            return _mapper.Map<List<OvertimeToGet>>(await _overtimeRepository.GetAll());
        }

        public async Task Update(OvertimeToUpdate overtimeToUpdate)
        {
            await _overtimeRepository.Update(_mapper.Map<Overtime>(overtimeToUpdate));
        }
    }
}
