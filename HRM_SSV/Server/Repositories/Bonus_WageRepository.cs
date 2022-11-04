using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IBonus_WageRepository : IGenericRepository<Bonus_Wage>
    {
    }
    public class Bonus_WageRepository : GenericRepository<Bonus_Wage>, IBonus_WageRepository
    {
        private readonly HRM_DbContext _db;
        public Bonus_WageRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
