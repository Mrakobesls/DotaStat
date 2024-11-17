using CommonExtensions;

namespace Hero.Data.Repository;

public interface IDataHeroCommands
{
    Task AddRange(IEnumerable<Types.Hero> heroes);
}

public class DataHeroCommands(HeroesDbContext dbContext) : IDataHeroCommands
{
    public async Task AddRange(IEnumerable<Types.Hero> heroes)
    {
        await dbContext.Heroes.AddRangeAsync(
            heroes.Select(
                x => new Models.Hero
                {
                    Id = x.Id,
                    Name = x.Name
                }
            )
        );

        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        using var task = dbContext.StartIdentityInsert<Types.Hero>();

        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}
