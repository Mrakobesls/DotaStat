using DB_UnitOfWork.Inteface;
using DotaStat.Business.Interfaces;
using DotaStat.Business.Interfaces.Models;
using DotaStat.Data.EntityFramework.Model;
using System;
using System.Linq;

namespace DotaStat.Business
{
    public class WeekPatchService : GenericService, IWeekPatchService
    {
        public WeekPatchService(IUnitOfWork uow) : base(uow)
        {
        }

        public void UpdateCurrentWeek(Pack pack)
        {
            _uow.WeekPatches.Read(GetCurrentWeekId());
        }

        public int EnsureExisting(int weekId, string patch)
        {
            if (!_uow.WeekPatches.ReadAll().Any(x => x.WeekId == weekId && x.Patch == patch))
            {
                _uow.WeekPatches.Create(new WeekPatch() { WeekId = weekId, Patch = patch });

                _uow.SaveChanges();
            }

            return _uow.WeekPatches.ReadAll().First(x => x.WeekId == weekId && x.Patch == patch).Id;
        }

        public int GetCurrentWeekPatchId()
        {
            EnsureExisting(GetCurrentWeekId(), GetCurrentPatch());

            return _uow.WeekPatches.ReadAll().Max(x => x.WeekId);
        }

        public int GetCurrentWeekId()
        {
            var worldStartDate = new DateTime(1970, 1, 1);
            var timePassed = DateTime.Now - worldStartDate;

            return (int)(timePassed.TotalDays / 7);
        }

        public int GetNeededWeekId(int startTime)
        {
            var a = (int)((startTime - new DateTime(1970, 1, 1).Second) / 86400 / 7);
            return a;
        }

        public string GetDateByWeekPatchId(int weekPatchId)
        {
            var addSeconds = (Convert.ToInt64(_uow.WeekPatches.Read(weekPatchId).WeekId) * 7 * 86400);
            var date = new DateTime(1970, 1, 1).AddSeconds(addSeconds);
            return date.ToString("dd-MM-yyyy");
        }

        public void EnsureRelevance(int startTime)
        {
            var weekId = (int)(new TimeSpan(startTime).TotalDays / 7);
            EnsureExisting(weekId, GetCurrentPatch());
        }

        public string GetCurrentPatch() => "7.30е";
    }
}