using EventBus.Events;

namespace Hero.Business.IntegrationEvents.Events;

public record NewHeroesReleasedIntegrationEvent(Hero[] Heroes) : IntegrationEvent;

public record Hero(int OriginalId, string Name);
