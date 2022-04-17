using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IOvertimeRepository : IGenericRepository<Overtime>
    {
    }
    public class OvertimeRepository : GenericRepository<Overtime>, IOvertimeRepository
    {
        private readonly HRM_DbContext _db;
        public OvertimeRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
