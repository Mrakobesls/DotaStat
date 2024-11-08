using CommonExtensions;
using Statistics.Business.Infrastructure;
using Statistics.Business.Infrastructure.Models;

namespace Statistics.Business.Services;

public interface IItemCommands
{
    Task AddRange(IEnumerable<Types.Item> items);
}

public class ItemCommands(StatisticsDbContext dbContext) : IItemCommands
{
    public async Task AddRange(IEnumerable<Types.Item> items)
    {
        await dbContext.Items.AddRangeAsync(
            items.Select(
                x => new Item
                {
                    Id = x.Id,
                    Name = x.Name
                }
            )
        );

        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        using var task = dbContext.StartIdentityInsert<Item>();

        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}
