using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EthernalData.Models;
using EthernalData.Services;
using Nethereum.RPC.Eth.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EthernalData.Controllers
{
    public class HomeController : Controller
    {
        private readonly INethereumWeb3Service _nethereumService;
        private  IRopstenService _ropstenService;

        public HomeController(
            INethereumWeb3Service nethereumService,
            IRopstenService ropstenService)
        {
            _nethereumService = nethereumService;
            _ropstenService = ropstenService;
        }

        public IActionResult Index()
        {
            var g =  _ropstenService.GetTransactionsAsync();
            return View();
        }

        public IActionResult About()
        {
            
            LinkedList<Transaction> list = _nethereumService.GetTransactionsByAddress("0xfB40701afA82e807A7dE7C112D3f26A4361b8A29", 5085560, 5085570);
            Console.WriteLine(list.Count);
       

            return View(list);
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
