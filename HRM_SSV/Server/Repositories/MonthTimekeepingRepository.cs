using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IMonthTimekeepingRepository : IGenericRepository<MonthTimeKeeping>
    {
    }
    public class MonthTimekeepingRepository : GenericRepository<MonthTimeKeeping>, IMonthTimekeepingRepository
    {
        private readonly HRM_DbContext _db;
        public MonthTimekeepingRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
