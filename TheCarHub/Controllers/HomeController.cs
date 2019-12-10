﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheCarHub.Models.Entities;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IMapper _mapper;

        public HomeController(IListingService listingService, IMapper mapper)
        {
            _listingService = listingService;
            _mapper = mapper;
        }

        // GET: /
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["SearchString"] = searchString;

//            if (string.IsNullOrEmpty(searchString))
//            {
//                listingViewModels = await _listingService.GetAllListingsAsViewModel();
//            }
//            else
//            {
                var listingViewModels = await _listingService
                    .GetFilteredListingViewModels(1, searchString);
//            }

            return View(listingViewModels);
        }

        // GET: Privacy/
        public IActionResult Privacy()
        {
            return View();
        }
        
        // GET: Listing/5
        public async Task<IActionResult> Listing(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var viewModel = await _listingService.GetListingViewModelByIdAsync(id.GetValueOrDefault());

            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
