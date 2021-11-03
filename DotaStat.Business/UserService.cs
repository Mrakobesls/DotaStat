using DB_UnitOfWork.Inteface;
using DotaStat.Data.EntityFramework.Model;
using System.Collections.Generic;
using System.Linq;

namespace DotaStat.Business
{
    public class UserService : GenericService
    {
        public UserService(IUnitOfWork uow) : base(uow)
        {
        }
        public List<User> GetUsers()
        {
            return _uow.Users.ReadAll().ToList();
        }
    }
}
