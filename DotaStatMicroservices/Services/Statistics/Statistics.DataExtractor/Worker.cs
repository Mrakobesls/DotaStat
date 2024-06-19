using System.Diagnostics;
using Microsoft.Extensions.Options;
using Statistics.Business.Enums;
using Statistics.Business.Services;
using Statistics.Business.Types;
using Statistics.DataExtractor.Configuration;

namespace Statistics.DataExtractor;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly SteamHttpClient _steamHttpClient;
    private readonly ILogger<Worker> _logger;

    private IHeroStatisticsService _heroStatisticsService;
    private IWeekPatchService _weekPatchService;

    private string? _lastMatchSeqNum; //= "5154074033";//"4667818086";

    private string? LastMatchSeqNum
    {
        get
        {
            if (_lastMatchSeqNum is null)
            {
                var lastMatch = _steamHttpClient.GetLastMatchSeqNum();
                lastMatch.Wait();

                _lastMatchSeqNum = lastMatch.Result;
            }

            Console.WriteLine(_lastMatchSeqNum);
            Debug.WriteLine(_lastMatchSeqNum);

            return _lastMatchSeqNum;
        }
        set => _lastMatchSeqNum = value;
    }

    public Worker(
        IServiceScopeFactory serviceScopeFactory,
        SteamHttpClient steamHttpClient,
        IOptions<StartupConfiguration> startupConfiguration,
        ILogger<Worker> logger
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _steamHttpClient = steamHttpClient;
        LastMatchSeqNum = startupConfiguration.Value.LastMatchSeqNum;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        _heroStatisticsService = scope.ServiceProvider.GetService<IHeroStatisticsService>()!;
        _weekPatchService = scope.ServiceProvider.GetService<IWeekPatchService>()!;

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await DoWork();
        }
    }

    private async Task DoWork()
    {
        var lastMatches = await _steamHttpClient.GetLast100Matches(LastMatchSeqNum);
        if (lastMatches is null)
            return;

        LastMatchSeqNum = lastMatches.Result.Matches[^1].MatchSeqNum.ToString();

        // filter ranked matches only
        lastMatches.Result.Matches = lastMatches.Result.Matches[1..]
            .Where(x => x.LobbyType == 7)
            .ToArray();

        var matches = lastMatches.Result.Matches.ToList();
        var badMatches = matches.Where(
                match => match.Players
                    .Any(player => player.LeaverStatus != 0 || player.HeroId == 0)
            )
            .ToArray();
        foreach (var badMatch in badMatches)
        {
            matches.Remove(badMatch);
        }

        lastMatches.Result.Matches = matches.ToArray();

        // create pack with needed fields
        var pack = CreatePack(lastMatches);
        var weekPatchId = _weekPatchService.EnsureExists(
            _weekPatchService.GetWeekId(lastMatches.Result.Matches.First().StartTime),
            _weekPatchService.GetCurrentPatch()
        );

        await _heroStatisticsService.AddPackResults(pack, weekPatchId);
    }

    private Pack CreatePack(LastMatchesRequest lastMatchesRequest)
    {
        var pack = new Pack();
        foreach (var match in lastMatchesRequest.Result.Matches)
        {
            for (var i = 0; i < match.Players.Length; i++)
            {
                var firstPlayer = match.Players[i];

                for (var j = i + 1; j < match.Players.Length; j++)
                {
                    var secondPlayer = match.Players[j];

                    if (firstPlayer.HeroId == 0 || secondPlayer.HeroId == 0)
                    {
                        Debug.WriteLine("Hero error" + firstPlayer.HeroId + " " + secondPlayer.HeroId);
                    }

                    var heroRelations = IsAllies(firstPlayer.PlayerSlot, secondPlayer.PlayerSlot)
                        ? HeroRelations.Allies
                        : HeroRelations.Enemies;
                    var isFirstHeroWin = match.RadiantWin ^ IsRadiant(firstPlayer.PlayerSlot)
                        ? MatchResult.Lose
                        : MatchResult.Win;

                    pack.HeroWinCouples.Add(
                        new HeroMatchResult
                        {
                            FirstHero = firstPlayer.HeroId,
                            SecondHero = secondPlayer.HeroId,
                            HeroRelations = heroRelations,
                            MatchResult = isFirstHeroWin
                        }
                    );
                }
            }
        }

        return pack;
    }

    private bool IsAllies(int firstHeroSlot, int secondHeroSlot)
    {
        return Math.Abs(firstHeroSlot - secondHeroSlot) < 5;
    }

    private bool IsRadiant(int heroSlot)
    {
        return heroSlot < 5;
    }
}
