using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCarHub.Models.InputModels;
using TheCarHub.Services;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ListingController : Controller
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
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
            if (inputModel == null)
            {
                return RedirectToAction(nameof(Create), "Listing");
            }

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }
            
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

            var inputModel = await _listingService.GetListingInputModelByIdAsync(id.GetValueOrDefault());

            if (inputModel == null)
            {
                return NotFound();
            }

            return View(inputModel);
        }

        // POST: Listing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ListingInputModel inputModel)
        {
            if (inputModel == null)
            {
                return BadRequest();
            }
            
            if (id != inputModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var editSuccess = await _listingService.UpdateListingAsync(inputModel);

                if (editSuccess)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }

                TempData["EditError"] =
                    "There was a problem updating the information. Please ensure the data entered is valid, and try again.";
               
                return View(inputModel);
            }

            return View(inputModel);
        }
    }
}