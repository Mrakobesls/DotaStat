using DotaStat.Business.Interfaces.Models;

namespace DotaStat.Business.Interfaces
{
    public interface IWeekPatchService
    {
        void UpdateCurrentWeek(Pack pack);
        public void EnsureExisting(int weekId, string patch);
        public int GetCurrentWeekId();
        public int GetCurrentWeekPatchId();
    }
}
