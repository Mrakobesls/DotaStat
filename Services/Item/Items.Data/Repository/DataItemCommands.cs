using Items.Data.Types;

namespace Items.Data.Repository;

public interface IDataHeroCommands
{
    Task AddRange(IEnumerable<Hero> heroes);
}

public class DataItemCommands(ItemDbContext dbContext) : IDataHeroCommands
{
    public async Task AddRange(IEnumerable<Hero> heroes)
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
        using var task = dbContext.StartIdentityInsert<Hero>();

        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}
