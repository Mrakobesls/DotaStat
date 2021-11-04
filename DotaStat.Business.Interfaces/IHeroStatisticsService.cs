using System.Collections.Generic;
using DotaStat.Business.Interfaces.Models;
using DotaStat.Data.EntityFramework.Model;

namespace DotaStat.Business.Interfaces
{
    public interface IHeroStatisticsService
    {
        void AddPackResults(Pack pack, int weekPatchId);
        void Squash(int tabletWeekPatch);
        List<WeeklyWinrate> getHeroWRHistory(int heroId);
    }
}
