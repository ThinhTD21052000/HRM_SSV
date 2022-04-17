using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
    }
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly HRM_DbContext _db;
        public RoleRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
