using System.Collections.Generic;
using System.Linq;
using DataBase.Model;
using DB_UnitOfWork.Inteface;

namespace DotaStat.Business
{
    class ItemService : GenericService
    {
        public ItemService(IUnitOfWork eow) : base(eow)
        {
        }
        public List<Item> GetItems()
        {
            return _eow.Items.ReadAll().ToList();
        }
    }
}
