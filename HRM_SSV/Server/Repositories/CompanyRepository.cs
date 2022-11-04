using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
    }
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly HRM_DbContext _db;
        public CompanyRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
