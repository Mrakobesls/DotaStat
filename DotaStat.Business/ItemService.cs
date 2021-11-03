using System.Collections.Generic;
using System.Linq;
using DataBase.Model;
using DB_UnitOfWork.Inteface;

namespace DotaStat.Business
{
    class ItemService : GenericService
    {
        public ItemService(IUnitOfWork uow) : base(uow)
        {
        }
        public List<Item> GetItems()
        {
            return _uow.Items.ReadAll().ToList();
        }
    }
}
