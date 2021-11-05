using DotaStat.Data.EntityFramework.Model;

namespace DotaStat.Data.EntityFramework.Repositories
{
    interface IWeekPatchesRepository : ICreate<WeekPatch>, IRead<WeekPatch>, IUpdate<WeekPatch>
    {
    }
}
