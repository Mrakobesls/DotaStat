using DotaStat.Business.Interfaces.Models;

namespace DotaStat.Business.Interfaces
{
    public interface IHeroStatisticsService
    {
        void AddPackResults(Pack pack);
        void Squash();
    }
}
