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
            
            var listings = await _listingService.GetAllListings();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                listings = listings.Where(l => l.Status.Id == 1 && l.Car.Make.Contains(searchString));
            }

            listings = listings.Where(l => l.Status.Id == 1);

            var models = _mapper.Map<IList<Listing>, IList<ListingViewModel>>(listings.ToList());
            
            return View(models);
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

            var listing = await _listingService.GetListingByIdAsync(id.GetValueOrDefault());

            if (listing == null)
                return NotFound();

            var model = _mapper.Map<ListingViewModel>(listing);

            return View(model);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
