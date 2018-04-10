﻿using System;
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
using EthernalData.Models.HomeViewModels;

using EthernalData.Converters;

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

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

     
        public async Task<IActionResult> ImageOverview(string searchString, string currentFilter, int? page)
        {
            var user = await _userManager.GetUserAsync(User);
            IQueryable<Domain.Transaction> transactions = await _EtherScanAPIService.GetTransactionsAsync(user.ETHAddress, 2913507, 4300000, Sort.ASC);


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                transactions = transactions.Where(s => s.BlockNumber.Value.ToString().Contains(searchString)
                                       || s.Nonce.Value.ToString().Contains(searchString)).AsQueryable();
            }

            if (user != null)
            {
                Debug.WriteLine(user.ETHAddress);
            }

            int pageSize = 3;
            return View(await PaginatedList<Domain.Transaction>.CreateAsync(transactions.AsQueryable(), page ?? 1, pageSize));
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

        [Authorize]
        [HttpGet]
        public IActionResult CheckTransaction()
        {            
            return View(new CheckTransactionViewModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CheckTransaction(CheckTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }           

            model.Transaction = await _nethereumService.GetTransactionByHashAsync(model.TXHash.Trim());
            if (model.Transaction.BlockHash == null)
            {
                model.StatusMessage = "Error: Something went wrong. Is the transaction hash valid?";
            } else
            {
                model.StatusMessage = "Retrieved transaction";
                model.HasTransaction = true;
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
