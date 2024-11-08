using CommonExtensions;
using Microsoft.EntityFrameworkCore;
using Statistics.Business.Infrastructure;
using Statistics.Business.Infrastructure.Models;

namespace Statistics.Business.Services;

public interface IHeroService
{
    public Task<List<Hero>> GetAll();
}

public class HeroQueries : IHeroService
{
    private readonly StatisticsDbContext _dbContext;

    public HeroQueries(StatisticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Hero>> GetAll()
    {
        return await _dbContext.Heroes.ToListAsync();
    }
}
