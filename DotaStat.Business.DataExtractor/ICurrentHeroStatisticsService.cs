using DotaStat.Business.Interfaces.Types;

namespace DotaStat.Business.Interfaces
{
    public interface ICurrentHeroStatisticsService
    {
        void AddMatchResult(Pack pack);
    }
}
