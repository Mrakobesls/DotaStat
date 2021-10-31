using DB_UnitOfWork.Inteface;

namespace DotaStat.Business
{
    public class GenericService
    {
        public readonly IUnitOfWork _eow;
        public GenericService(IUnitOfWork eow)
        {
            _eow = eow;
        }
    }
}
