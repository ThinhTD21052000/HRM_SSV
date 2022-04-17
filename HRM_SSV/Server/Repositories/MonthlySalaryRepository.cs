using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IMonthlySalaryRepository : IGenericRepository<MonthlySalary>
    {
    }
    public class MonthlySalaryRepository : GenericRepository<MonthlySalary>, IMonthlySalaryRepository
    {
        private readonly HRM_DbContext _db;
        public MonthlySalaryRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
