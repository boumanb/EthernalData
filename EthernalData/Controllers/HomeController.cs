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
using Microsoft.AspNetCore.Identity;
using EthernalData.Models.ManageViewModels;

namespace EthernalData.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly INethereumWeb3Service _nethereumService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEtherScanAPIService _EtherScanAPIService;


        public HomeController(
            INethereumWeb3Service nethereumService,
            IEtherScanAPIService etherScanAPIService,
            UserManager<ApplicationUser> userManager)
        {
            _nethereumService = nethereumService;
            _EtherScanAPIService = etherScanAPIService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var g = _EtherScanAPIService.GetTransactionsAsync("0x1Fb65D5a17571433e6fb5e8119251348FEA23140", 2913507, 2913599, Sort.ASC);
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            
            LinkedList<Transaction> list = _nethereumService.GetTransactionsByAddress("0xfB40701afA82e807A7dE7C112D3f26A4361b8A29", 5085560, 5085570);
            Console.WriteLine(list.Count);
       

            return View(list);
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new UploadViewModel
            {
                ETHAddress = user.ETHAddress
            };
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
