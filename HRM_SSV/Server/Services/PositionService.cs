using AutoMapper;
using Domain.Modals.Position;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IPositionService
    {
        Task Create(PositionToAdd positionToAdd);
        Task Update(PositionToUpdate positionToUpdate);
        Task Delete(int id);
        Task<PositionToGet> Get(int id);
        Task<List<PositionToGet>> GetList();
    }
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;
        public PositionService(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task Create(PositionToAdd positionToAdd)
        {
            await _positionRepository.Add(_mapper.Map<Position>(positionToAdd));
        }

        public async Task Delete(int id)
        {
            await _positionRepository.Delete(_mapper.Map<Position>( await Get(id)));
        }

        public async Task<PositionToGet> Get(int id)
        {
            return _mapper.Map<PositionToGet>(await _positionRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<PositionToGet>> GetList()
        {
            return _mapper.Map<List<PositionToGet>>(await _positionRepository.GetAll());
        }

        public async Task Update(PositionToUpdate positionToUpdate)
        {
            await _positionRepository.Update(_mapper.Map<Position>(positionToUpdate));
        }
    }
}
