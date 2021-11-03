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

        public void EnsureExisting(int weekId, string patch)
        {
            if (_uow.WeekPatches.ReadAll().FirstOrDefault(x => x.WeekId == weekId && x.Patch == patch) is null)
            {
                _uow.WeekPatches.Create(new WeekPatch() { WeekId = weekId, Patch = patch });
            }
            _uow.SaveChanges();
        }

        public int GetCurrentWeekPatchId()
        {
            EnsureExisting(GetCurrentWeekId(), GetCurrentPatch());
            return _uow.WeekPatches.ReadAll().Max(x => x.Id);
        }

        public int GetCurrentWeekId()
        {
            var startDate = new DateTime(1970, 1, 1);
            var timePassed = DateTime.Now - startDate;

            return (int)(timePassed.TotalDays / 7);
        }

        public string GetCurrentPatch() => "7.30е";
    }
}