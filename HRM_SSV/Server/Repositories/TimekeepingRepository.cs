using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface ITimekeepingRepository : IGenericRepository<Timekeeping>
    {
    }
    public class TimekeepingRepository : GenericRepository<Timekeeping>, ITimekeepingRepository
    {
        private readonly HRM_DbContext _db;
        public TimekeepingRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
