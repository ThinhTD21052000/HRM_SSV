using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IWageRepository : IGenericRepository<Wage>
    {
    }
    public class WageRepository : GenericRepository<Wage>, IWageRepository
    {
        private readonly HRM_DbContext _db;
        public WageRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
