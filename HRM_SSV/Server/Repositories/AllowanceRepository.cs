using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IAllowanceRepository : IGenericRepository<Allowance>
    {
    }
    public class AllowanceRepository : GenericRepository<Allowance>, IAllowanceRepository
    {
        private readonly HRM_DbContext _db;
        public AllowanceRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
