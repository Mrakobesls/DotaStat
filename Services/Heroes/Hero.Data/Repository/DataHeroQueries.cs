using Microsoft.EntityFrameworkCore;

namespace Hero.Data.Repository;

public interface IDataHeroQueries
{
    Task<int> GetCountAsync();
    Task<IEnumerable<Types.Hero>> GetAllAsync();
}

public class DataHeroQueries(HeroesDbContext dbContext) : IDataHeroQueries
{
    public async Task<int> GetCountAsync()
    {
        return await dbContext.Heroes.CountAsync();
    }

    public async Task<IEnumerable<Types.Hero>> GetAllAsync()
    {
        return await dbContext.Heroes.Select(
                x => new Types.Hero
                {
                    Id = x.Id,
                    Name = x.Name
                }
            )
            .ToArrayAsync();
    }
}
