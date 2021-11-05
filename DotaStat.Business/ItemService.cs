using System.Collections.Generic;
using System.Linq;
using DotaStat.Data.EntityFramework.Model;
using DotaStat.Data.EntityFramework.UnitOfWork;

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
