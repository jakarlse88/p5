﻿using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IListingService _listingService;

        public HomeController(IListingService listingService)
        {
            _listingService = listingService;
        }

        // GET: /
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["SearchString"] = searchString;

            var listingViewModels = await _listingService
                .GetFilteredListingViewModels(1, searchString);

            return View(listingViewModels);
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
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
