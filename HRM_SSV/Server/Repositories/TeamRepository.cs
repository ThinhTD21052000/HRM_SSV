using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
    }
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly HRM_DbContext _db;
        public TeamRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
