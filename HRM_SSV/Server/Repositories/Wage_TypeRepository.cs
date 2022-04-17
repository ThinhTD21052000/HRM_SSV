using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IWage_TypeRepository : IGenericRepository<Wage_Type>
    {
    }
    public class Wage_TypeRepository : GenericRepository<Wage_Type>, IWage_TypeRepository
    {
        private readonly HRM_DbContext _db;
        public Wage_TypeRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
