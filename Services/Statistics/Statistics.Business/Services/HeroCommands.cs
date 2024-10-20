using CommonExtensions;
using Statistics.Business.Infrastructure;
using Statistics.Business.Infrastructure.Models;

namespace Statistics.Business.Services;

public interface IDataHeroCommands
{
    Task AddRange(IEnumerable<Types.Hero> heroes);
}

public class HeroCommands(StatisticsDbContext dbContext) : IDataHeroCommands
{
    public async Task AddRange(IEnumerable<Types.Hero> heroes)
    {
        await dbContext.Heroes.AddRangeAsync(
            heroes.Select(
                x => new Hero
                {
                    Id = x.Id,
                    Name = x.Name
                }
            )
        );

        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        using var task = dbContext.StartIdentityInsert<Hero>();

        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}
