using EventBus;
using EventBus.Abstractions;
using EventBus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace ServiceDefaults;

public static partial class Extensions
{
    public static IHostApplicationBuilder AddEventBus(this IHostApplicationBuilder builder)
    {
        // {
        //   "EventBus": {
        //     "ProviderName": "ServiceBus | RabbitMQ",
        //     ...
        //   }
        // }

        // {
        //   "EventBus": {
        //     "ProviderName": "ServiceBus",
        //     "SubscriptionClientName": "eshop_event_bus"
        //   }
        // }

        // {
        //   "EventBus": {
        //     "ProviderName": "RabbitMQ",
        //     "SubscriptionClientName": "...",
        //     "UserName": "...",
        //     "Password": "...",
        //     "RetryCount": 1
        //   }
        // }

        var eventBusSection = builder.Configuration.GetSection("EventBus");

        if (!eventBusSection.Exists())
        {
            return builder;
        }

        switch (eventBusSection["ProviderName"])
        {
            case "RabbitMQ":
                builder.Services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                    var factory = new ConnectionFactory
                    {
                        HostName = eventBusSection["Host"],
                        Port = eventBusSection.GetValue<int>("Port"),
                        DispatchConsumersAsync = true
                    };

                    if (!string.IsNullOrEmpty(eventBusSection["UserName"]))
                    {
                        factory.UserName = eventBusSection["UserName"];
                    }

                    if (!string.IsNullOrEmpty(eventBusSection["Password"]))
                    {
                        factory.Password = eventBusSection["Password"];
                    }

                    var retryCount = eventBusSection.GetValue("RetryCount", 5);

                    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
                });

                builder.Services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
                {
                    var subscriptionClientName = eventBusSection.GetRequiredValue("SubscriptionClientName");
                    var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
                    var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                    var retryCount = eventBusSection.GetValue("RetryCount", 5);

                    return new EventBusRabbitMq(rabbitMqPersistentConnection, logger, sp, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
                });
                break;
            // TODO Implement ServiceBus
            // case "ServiceBus":
            //     builder.Services.AddSingleton<IServiceBusPersistentConnection>(sp =>
            //     {
            //         var serviceBusConnectionString = builder.Configuration.GetRequiredConnectionString("EventBus");
            //
            //         return new DefaultServiceBusPersistentConnection(serviceBusConnectionString);
            //     });
            //
            //     builder.Services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            //     {
            //         var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersistentConnection>();
            //         var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
            //         var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            //         var subscriptionName = eventBusSection.GetRequiredValue("SubscriptionClientName");
            //
            //         return new EventBusServiceBus(serviceBusPersisterConnection, logger,
            //             eventBusSubscriptionsManager, sp, subscriptionName);
            //     });
            //     break;
        }

        builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        return builder;
    }
}
