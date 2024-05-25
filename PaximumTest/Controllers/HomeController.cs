using Microsoft.AspNetCore.Mvc;
using PaximumTest.Models;
using PaximumTest.PxmModels.HotelListModels;
using PaximumTest.PxmModels.LoginModels;
using System.Diagnostics;

namespace PaximumTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            LoginClientProcess loginClient = new LoginClientProcess();
            var token = loginClient.GetToken().Result;
            if (token is not null)
            {
                HotelListProcess hotelListprocess = new HotelListProcess();
                var list = hotelListprocess.GetHotelList(token);
         
                return View(list);
            }
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
