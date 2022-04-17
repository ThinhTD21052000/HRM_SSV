using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface ILaborContractRepository : IGenericRepository<LaborContract>
    {
    }
    public class LaborContractRepository : GenericRepository<LaborContract>, ILaborContractRepository
    {
        private readonly HRM_DbContext _db;
        public LaborContractRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
