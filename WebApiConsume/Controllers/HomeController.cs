using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApiConsume.Models;
using WebApiConsume.Models.ViewModel;
using WebApiConsume.Repository.IRepository;

namespace WebApiConsume.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly ITrailsRepository _trailsRepository;

        public HomeController(ILogger<HomeController> logger, INationalParkRepository nationalParkRepository, ITrailsRepository trailsRepository)
        {
            _logger = logger;
            _nationalParkRepository = nationalParkRepository;
            _trailsRepository = trailsRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM indexVM = new IndexVM()
            {
                TrailsList = await _trailsRepository.GetAllAsync(SD.TrailsPath),
                NationalParksList = await _nationalParkRepository.GetAllAsync(SD.NationalParkPath)
            };
            return View(indexVM);
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
