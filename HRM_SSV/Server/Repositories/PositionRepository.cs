using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
    }
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        private readonly HRM_DbContext _db;
        public PositionRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
