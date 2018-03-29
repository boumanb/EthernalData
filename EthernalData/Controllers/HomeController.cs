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
            return View();
        }

     
        public async Task<IActionResult> ImageOverview()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            ApplicationUser user = await _userManager.GetUserAsync(currentUser);
            List<Domain.Transaction> transactions = await _EtherScanAPIService.GetTransactionsAsync(user.ETHAddress, 2913507, 4300000, Sort.ASC);
            if (user != null)
            {
                Debug.WriteLine(user.ETHAddress);
            }

            return View(transactions);
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
