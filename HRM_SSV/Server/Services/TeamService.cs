using AutoMapper;
using Domain.Modals.Team;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface ITeamService
    {
        Task Create(TeamToAdd teamToAdd);
        Task Update(TeamToUpdate teamToUpdate);
        Task Delete(int id);
        Task<TeamToGet> Get(int id);
        Task<List<TeamToGet>> GetList();
    }
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task Create(TeamToAdd TeamToAdd)
        {
            await _teamRepository.Add(_mapper.Map<Team>(TeamToAdd));
        }

        public async Task Delete(int id)
        {
            await _teamRepository.Delete(await _teamRepository.Get(x => x.Id == id));
        }

        public async Task<TeamToGet> Get(int id)
        {
            return _mapper.Map<TeamToGet>(await _teamRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<TeamToGet>> GetList()
        {
            return _mapper.Map<List<TeamToGet>>(await _teamRepository.GetAll());
        }

        public async Task Update(TeamToUpdate teamToUpdate)
        {
            await _teamRepository.Update(_mapper.Map<Team>(teamToUpdate));
        }
    }
}
