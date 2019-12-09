using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Models.InputModels;
using TheCarHub.Services;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ListingController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IMapper _mapper;

        public ListingController(
            IListingService listingService,
            IMapper mapper)
        {
            _listingService = listingService;
            _mapper = mapper;
        }

        // GET: Listing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Listing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListingInputModel inputModel)
        {
            if (!ModelState.IsValid) return View(inputModel);
            
            await _listingService.AddListingAsync(inputModel);

            return RedirectToAction(nameof(Index), "Home");

        }

        // GET: Listing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing =
                await _listingService.GetListingByIdAsync(id.GetValueOrDefault());

            if (listing == null)
            {
                return NotFound();
            }

            // TODO: extract to service
            var inputModel = _mapper.Map<ListingInputModel>(listing);

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
                // TODO: extact all logic to service
                var listing = await _listingService.GetListingByIdAsync(id);

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

            return View(inputModel);
        }

        private async Task<bool> ListingExists(int id)
        {
            var listings = await _listingService.GetAllListings();

            return listings.Any(e => e.Id == id);
        }
    }
}