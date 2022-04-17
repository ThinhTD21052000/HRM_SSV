using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
    }
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        private readonly HRM_DbContext _db;
        public MenuRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
