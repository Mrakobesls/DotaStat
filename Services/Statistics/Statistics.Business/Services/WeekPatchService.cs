using Statistics.Business.Model;
using Statistics.Business.Types;

namespace Statistics.Business.Services;

public interface IWeekPatchService
{
    public int GetCurrentWeekId();
    public int GetCurrentWeekPatchId();
    string GetDateByWeekPatchId(int weekPatchId);
    int GetWeekId(int startTime);
    string GetCurrentPatch();
    public int EnsureExists(int weekId, string patch);
    void EnsureRelevance(int startTime);
}

public class WeekPatchService : IWeekPatchService
{
    private readonly DotaStatDbContext _dbContext;

    public WeekPatchService(DotaStatDbContext dbContext)
    {
            _dbContext = dbContext;
        }

    public int GetCurrentWeekPatchId()
    {
            EnsureExists(GetCurrentWeekId(), GetCurrentPatch());

            return _dbContext.WeekPatches.Max(x => x.WeekId);
        }

    public int GetCurrentWeekId()
    {
            var worldStartDate = new DateTime(1970, 1, 1);
            var timePassed = DateTime.Now - worldStartDate;

            return (int)(timePassed.TotalDays / 7);
        }

    public int GetWeekId(int startTime)
    {
            return (startTime - new DateTime(1970, 1, 1).Second) / 86400 / 7;
        }

    public string GetDateByWeekPatchId(int weekPatchId)
    {
            var addSeconds = (Convert.ToInt64(_dbContext.WeekPatches.Find(weekPatchId)?.WeekId) * 7 * 86400);
            var date = new DateTime(1970, 1, 1).AddSeconds(addSeconds);
            return date.ToString("dd-MM-yyyy");
        }

    public string GetCurrentPatch()
    {
            return "7.35b";
        }

    public int EnsureExists(int weekId, string patch)
    {
            if (!_dbContext.WeekPatches.Any(x => x.WeekId == weekId && x.Patch == patch))
            {
                _dbContext.WeekPatches.Add(new WeekPatch { WeekId = weekId, Patch = patch });

                _dbContext.SaveChanges();
            }

            return _dbContext.WeekPatches.First(x => x.WeekId == weekId && x.Patch == patch).Id;
        }

    public void EnsureRelevance(int startTime)
    {
            var weekId = (int)(new TimeSpan(startTime).TotalDays / 7);
            EnsureExists(weekId, GetCurrentPatch());
        }
}