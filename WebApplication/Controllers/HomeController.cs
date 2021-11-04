using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DotaStat.Business.Interfaces;
using Newtonsoft.Json;
using WebApplication.Models;
using WebApplication.ViewModel;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHeroStatisticsService _heroStatisticsService;

        public HomeController(IHeroStatisticsService heroStatisticsService)
        {
            _heroStatisticsService = heroStatisticsService;
        }

        public IActionResult Index(int heroId = 1)
        {
            var dataPoints = new List<DataPoint>();
            //{

            //    new DataPoint("haha", 22),
            //    new DataPoint("hehe", 36),
            //    new DataPoint("hoho", 42),
            //    new DataPoint("40", 51),
            //    new DataPoint("50", 46),
            //};
            _heroStatisticsService.getHeroWRHistory(heroId).Select(dp=> new DataPoint(dp.WeekPatchId, $"{(double)dp.Wins / dp.AllGames * 100:f2}%"));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
