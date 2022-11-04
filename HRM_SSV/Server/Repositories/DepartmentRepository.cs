using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
    }
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly HRM_DbContext _db;
        public DepartmentRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
