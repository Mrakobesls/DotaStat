using Dapper;
using EventBus.Abstractions;
using Item.Business.Infrastructure;
using Item.Business.IntegrationEvents.Events;

namespace Item.Business.Services;

public interface IItemCommands
{
    Task EnsureAllExist();
}

public class ItemCommands(
    IItemQueries itemQueries,
    IIntegrationEventService integrationEventService,
    OpenDotaHttpClient steamHttpClient,
    IDbConnectionFactory dbConnectionFactory
) : IItemCommands
{
    public async Task EnsureAllExist()
    {
        var dbConnection = dbConnectionFactory.Create();

        var items = (await steamHttpClient.GetItems())
            .ToArray();

        var localItems = await itemQueries.GetAllAsync();

        var newItems = items.ExceptBy(localItems.Select(x => x.Id), x => x.Id)
            .Select(
                x => new Types.Item
                {
                    Id = x.Id,
                    Name = x.Name
                }
            )
            .ToArray();

        if (!newItems.Any())
        {
            return;
        }

        await dbConnection.ExecuteAsync(
            """
            -- noinspection SqlResolveForFile @ variable/"@Id"
            -- noinspection SqlResolveForFile @ variable/"@Name"
            INSERT INTO Item (Id, [Name])
            VALUES (@Id, @Name)
            """,
            newItems
        );
        await integrationEventService.Publish(
            new NewItemsReleasedIntegrationEvent(
                newItems.Select(x => new IntegrationEvents.Events.Item(x.Id, x.Name))
                    .ToArray()
            )
        );
    }
}
