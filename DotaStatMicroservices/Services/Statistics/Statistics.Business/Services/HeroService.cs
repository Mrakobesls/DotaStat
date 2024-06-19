using Statistics.Business.Model;

namespace Statistics.Business.Services
{
    public interface IHeroService
    {
        public List<Hero> GetAll();
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

            _dbContext.Heroes.Truncate();

            _dbContext.Heroes.AddRange(
                dotaHeroes.Select(
                    h => new Hero
                    {
                        Id = h.Id,
                        Name = h.LocalizedName
                    }
                )
            );

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            using (_dbContext.StartIdentityInsert<Hero>())
            {
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }

        public List<Hero> GetAll()
        {
            return _dbContext.Heroes.ToList();
        }
    }
}
