using DotaStat.Business.Interfaces.Models;

namespace DotaStat.Business.Interfaces
{
    public interface IWeekPatchService
    {
        void UpdateCurrentWeek(Pack pack);
        public int EnsureExisting(int weekId, string patch);
        public int GetCurrentWeekId();
        public int GetCurrentWeekPatchId();
        void EnsureRelevance(int startTime);
        int GetNeededWeekId(int startTime);
        string GetCurrentPatch();
    }
}
