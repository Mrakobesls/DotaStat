using DotaStat.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using WebApplication.Models;
using WebApplication.ViewModel;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHeroStatisticsService _heroStatisticsService;
        private readonly IWeekPatchService _weekPatchService;
        private readonly IHeroService _heroService;

        public HomeController(IHeroStatisticsService heroStatisticsService, IWeekPatchService weekPatchService, IHeroService heroService)
        {
            _heroStatisticsService = heroStatisticsService;
            _weekPatchService = weekPatchService;
            _heroService = heroService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HeroStatistic()
        {
            ViewBag.AllHeroes = _heroService.GetAllHeroes().Select(h => new Hero() { Id = h.Id, Name = h.Name }).ToList();
            ViewBag.HeroName = "";

            return View(new Hero());
        }

        [HttpPost]
        public IActionResult HeroStatistic(Hero hero)
        {
            var dataPoints = _heroStatisticsService.getHeroWRHistory((int)hero.Id)
                .Select(dp => new DataPoint(_weekPatchService.GetDateByWeekPatchId(dp.WeekPatchId), (double)dp.Wins / dp.AllGames * 100))
                .ToList();
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            var allHeroes = _heroService.GetAllHeroes().ToList();
            ViewBag.AllHeroes = allHeroes;

            ViewBag.HeroName = allHeroes.First(x=>x.Id == hero.Id).Name;

            return View("HeroStatistic", hero);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
