using System;
using System.Linq;
using DotaStat.Business.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using DB_UnitOfWork.Inteface;

namespace DotaStat.DataExtractor
{
    public class Worker : BackgroundService
    {
        //private readonly IHeroStatisticsService _heroStatisticsService;
        //private readonly IHeroService _heroService;
        //private readonly IUnitOfWork _uow;

        public Worker(/*IHeroStatisticsService heroStatisticsService, IUnitOfWork uow, IHeroService heroService*/)
        {
            //_heroStatisticsService = heroStatisticsService;
            //_uow = uow;
            //_heroService = heroService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_heroStatisticsService.Squash();
            //var heroes = _heroService.GetAllHeroes().ToList();
            //int i = 0;
            //foreach (var record in _uow.WeeklyWRs.ReadAll())
            //{
            //    Console.WriteLine($"{heroes[i++].Name} {(double) record.Wins / record.AllGames * 100:f2}%");
            //}

            return Task.CompletedTask;
        }
    }
}
