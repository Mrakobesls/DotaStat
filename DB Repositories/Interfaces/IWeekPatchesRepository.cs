using DotaStat.Data.EntityFramework.Model;

namespace DB_Repositories.Interfaces
{
    interface IWeekPatchesRepository : ICreate<WeekPatch>, IRead<WeekPatch>, IUpdate<WeekPatch>
    {
    }
}
