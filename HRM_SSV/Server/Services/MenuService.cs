using AutoMapper;
using Domain.Modals.Menu;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface IMenuService
    {
        Task Create(MenuToAdd menuToAdd);
        Task Update(MenuToUpdate menuToUpdate);
        Task Delete(int id);
        Task<MenuToGet> Get(int id);
        Task<List<MenuToGet>> GetList();
    }
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public MenuService(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task Create(MenuToAdd menuToAdd)
        {
            await _menuRepository.Add(_mapper.Map<Menu>(menuToAdd));
        }

        public async Task Delete(int id)
        {
            await _menuRepository.Delete(_mapper.Map<Menu>( await Get(id)));
        }

        public async Task<MenuToGet> Get(int id)
        {
            return _mapper.Map<MenuToGet>(await _menuRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<MenuToGet>> GetList()
        {
            return _mapper.Map<List<MenuToGet>>(await _menuRepository.GetAll());
        }

        public async Task Update(MenuToUpdate menuToUpdate)
        {
            await _menuRepository.Update(_mapper.Map<Menu>(menuToUpdate));
        }
    }
}
