using Microsoft.EntityFrameworkCore;
using Statistics.Business.Model;

namespace Statistics.Business.Services;

public interface IHeroService
{
    public Task<List<Hero>> GetAllAsync();
    public Task EnsureAllExist();
}

public class HeroService : IHeroService
{
    private readonly DotaStatDbContext _dbContext;
    private readonly SteamHttpClient _httpClient;

    public HeroService(DotaStatDbContext dbContext, SteamHttpClient httpClient)
    {
            _dbContext = dbContext;
            _httpClient = httpClient;
        }

    public async Task EnsureAllExist()
    {
            var dotaHeroes = (await _httpClient.GetHeroes())
                .Result.Heroes.OrderBy(x => x.Id)
                .ToArray();
            var localHeroesCount = _dbContext.Heroes.Count();

            if (dotaHeroes.Length == localHeroesCount)
                return;

            var localHeroes = _dbContext.Heroes.ToArray();

            var newHeroes = dotaHeroes.Select(x => x.Id).Except(localHeroes.Select(x => x.Id));
            _dbContext.Heroes.AddRange(
                dotaHeroes.Where(x => newHeroes.Contains(x.Id))
                    .Select(
                        h => new Hero
                        {
                            Id = h.Id,
                            Name = h.LocalizedName
                        }
                    )
            );

            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            using (_dbContext.StartIdentityInsert<Hero>())
            {
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }

    public async Task<List<Hero>> GetAllAsync()
    {
            return await _dbContext.Heroes.ToListAsync();
        }
}