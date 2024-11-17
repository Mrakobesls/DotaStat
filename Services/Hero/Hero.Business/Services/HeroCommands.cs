using EventBus.Abstractions;
using Hero.Business.IntegrationEvents.Events;
using Hero.Data.Repository;

namespace Hero.Business.Services;

public interface IHeroCommands
{
    public Task<IEnumerable<Types.Hero>> GetAllAsync();
    public Task EnsureAllExist();
}

public class HeroCommands(
    IDataHeroCommands dataHeroCommands,
    IDataHeroQueries dataHeroQueries,
    SteamHttpClient steamHttpClient,
    IIntegrationEventService integrationEventService
) : IHeroCommands
{
    public async Task EnsureAllExist()
    {
        var heroes = (await steamHttpClient.GetHeroes())
            .Result.Heroes.OrderBy(x => x.Id)
            .ToArray();
        var localHeroesCount = await dataHeroQueries.GetCountAsync();

        if (heroes.Length == localHeroesCount)
            return;

        var localHeroes = await dataHeroQueries.GetAllAsync();

        var newHeroes = heroes.ExceptBy(localHeroes.Select(x => x.Id), x => x.Id)
            .Select(
                h => new Data.Types.Hero
                {
                    Id = h.Id,
                    Name = h.LocalizedName
                }
            )
            .ToArray();

        if (!newHeroes.Any())
        {
            return;
        }

        await dataHeroCommands.AddRange(newHeroes);
        await integrationEventService.Publish(
            new NewHeroesReleasedIntegrationEvent(
                newHeroes.Select(x => new IntegrationEvents.Events.Hero(x.Id, x.Name))
                    .ToArray()
            )
        );
    }

    public async Task<IEnumerable<Types.Hero>> GetAllAsync()
    {
        return (await dataHeroQueries.GetAllAsync())
            .Select(
                h => new Types.Hero
                {
                    Id = h.Id,
                    Name = h.Name
                }
            );
    }
}
