using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IViolationMoneyRepository : IGenericRepository<ViolationMoney>
    {
    }
    public class ViolationMoneyRepository : GenericRepository<ViolationMoney>, IViolationMoneyRepository
    {
        private readonly HRM_DbContext _db;
        public ViolationMoneyRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
