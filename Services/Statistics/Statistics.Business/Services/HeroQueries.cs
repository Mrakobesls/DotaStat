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
    private readonly SteamHttpClient _httpClient;

    public HeroQueries(StatisticsDbContext dbContext, SteamHttpClient httpClient)
    {
        _dbContext = dbContext;
        _httpClient = httpClient;
    }

    public async Task<List<Hero>> GetAll()
    {
        return await _dbContext.Heroes.ToListAsync();
    }
}
