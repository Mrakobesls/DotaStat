using DataBase.Model;
using DotaStat.Data.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;

namespace DB_Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override void Delete(User user)
        {
            user.IsDeleted = true;
            _context.Update(user);
        }
    }
}
