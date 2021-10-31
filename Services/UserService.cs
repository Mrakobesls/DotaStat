using DB_UnitOfWork.Inteface;
using DotaStat.Data.EntityFramework.Model;
using System.Collections.Generic;
using System.Linq;

namespace DotaStat.Business
{
    public class UserService : GenericService
    {
        public UserService(IUnitOfWork eow) : base(eow)
        {
        }
        public List<User> GetUsers()
        {
            return _eow.Users.ReadAll().ToList();
        }
    }
}
