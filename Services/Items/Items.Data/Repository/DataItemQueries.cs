using Items.Data.Types;

namespace Items.Data.Repository;

public interface IDataHeroQueries
{
    Task<int> CountAsync();
    Task<IEnumerable<Hero>> AllAsync();
}

public class DataItemQueries(ItemDbContext dbContext) : IDataHeroQueries
{
    public async Task<int> CountAsync()
    {
        return (await dbContext.Heroes.CountAsync());
    }

    public async Task<IEnumerable<Hero>> AllAsync()
    {
        return (await dbContext.Heroes.Select(
                x => new Hero
                {
                    Id = x.Id,
                    Name = x.Name
                }
            )
            .ToArrayAsync());
    }
}
