using Microsoft.EntityFrameworkCore;
using Server.DBContext;
using Server.Entities;

namespace Server.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Login(string username, string password);
    }
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly HRM_DbContext _db;
        public UserRepository(HRM_DbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<User> Login(string username, string password)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.UserName.Equals(username) && x.PasswordHash.Equals(password));
        }
    }
}
