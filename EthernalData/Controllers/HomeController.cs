using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EthernalData.Models;
using EthernalData.Services;

namespace EthernalData.Controllers
{
    public class HomeController : Controller
    {
        private readonly INethereumWeb3Service _nethereumService;

        public HomeController(
            INethereumWeb3Service nethereumService)
        {
            _nethereumService = nethereumService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            
            var list = _nethereumService.GetTransactionsByAddress("0x1fb65d5a17571433e6fb5e8119251348fea23140", 2913521, 2913600);
            Console.WriteLine(list.Count);
            ViewData["Message"] = list.First.Value.TransactionHash;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
