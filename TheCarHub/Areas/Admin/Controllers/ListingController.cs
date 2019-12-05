using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ListingController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IListingService _listingService;
        private readonly ICarService _carService;
        private readonly IStatusService _statusService;
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;

        public ListingController(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            IListingService listingService,
            ICarService carService,
            IStatusService statusService,
            IMediaService mediaService,
            IMapper mapper)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _listingService = listingService;
            _carService = carService;
            _statusService = statusService;
            _mediaService = mediaService;
            _mapper = mapper;
        }

        // GET: Listing/Create
        public IActionResult Create()
        {
            var carYearSelect = PopulateCarYearSelect();

            ViewData["YearSelect"] =
                new SelectList(carYearSelect,
                    "Value",
                    "Text");

            return View();
        }

        // POST: Listing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListingInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                await _listingService.AddListingAsync(inputModel);

                return RedirectToAction(nameof(Index), "Home");
            }

            var carYearSelect = PopulateCarYearSelect();

            ViewData["YearSelect"] =
                new SelectList(carYearSelect,
                    "Value",
                    "Text");

            return View(inputModel);
        }

        // GET: Listing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing =
                await _listingService.GetListingById(id.GetValueOrDefault());

            if (listing == null)
            {
                return NotFound();
            }

            var inputModel = _mapper.Map<ListingInputModel>(listing);

            ViewData["YearSelect"] =
                new SelectList(PopulateCarYearSelect(),
                    "Value",
                    "Text");

            ViewData["StatusSelect"] =
                new SelectList(await PopulateStatusSelect(),
                    "Value",
                    "Text");

            return View(inputModel);
        }

        // POST: Listing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ListingInputModel inputModel)
        {
            if (id != inputModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var listing = await _listingService.GetListingById(id);

                try
                {
                    await _listingService.EditListing(inputModel, listing);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var listingExists = await ListingExists(inputModel.Id);

                    if (!listingExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), "Home");
            }

            ViewData["YearSelect"] =
                new SelectList(PopulateCarYearSelect(),
                    "Value",
                    "Text");

            ViewData["StatusSelect"] =
                new SelectList(await PopulateStatusSelect(),
                    "Value",
                    "Text");

            return View(inputModel);
        }

        private async Task<bool> ListingExists(int id)
        {
            var listings = await _listingService.GetAllListings();

            return listings.Any(e => e.Id == id);
        }

        /// <summary>
        /// Utility class for car year SelectList creation.
        /// </summary>
        private class YearSelectItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        /// <summary>
        /// Utility class for Status SelectList creation.
        /// </summary>
        private class StatusSelectItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        /// <summary>
        /// Utility method that populates a list with YearSelectItems
        /// representing years ranging from 1990 to current year + 1.
        /// </summary>
        /// <returns>A List of YearSelectItem containing entries from 1990 to current year +1, inclusive.</returns>
        private static IEnumerable<YearSelectItem> PopulateCarYearSelect()
        {
            var years = new List<YearSelectItem>();

            for (int i = 1990; i <= (DateTime.Today.Year + 1); i++)
            {
                years.Add(new YearSelectItem
                {
                    Value = i,
                    Text = i.ToString()
                });
            }

            return years;
        }

        /// <summary>
        /// Utility method that populates a list with StatusSelectItems representing
        /// all status options present in database.
        /// </summary>
        /// <returns>A List of YearSelectItem containing entries of all current status options.</returns>
        private async Task<IEnumerable<StatusSelectItem>> PopulateStatusSelect()
        {
            var statuses = await _statusService.GetAllStatuses();

            var statusSelectItems = new List<StatusSelectItem>();

            foreach (var item in statuses)
            {
                statusSelectItems.Add(new StatusSelectItem
                {
                    Value = item.Id,
                    Text = item.Name
                });
            }

            return statusSelectItems;
        }
    }
}