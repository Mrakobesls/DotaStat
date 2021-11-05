using DotaStat.Data.EntityFramework.UnitOfWork;

namespace DotaStat.Business
{
    public class GenericService
    {
        public readonly IUnitOfWork _uow;
        public GenericService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
