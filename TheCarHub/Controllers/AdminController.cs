using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheCarHub.Models;
using TheCarHub.Services;
using System.Linq;
using System;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TheCarHub.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ICarService _carService;
        private readonly IListingService _listingService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(ILogger<AdminController> logger,
            ICarService carService,
            IListingService listingService,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _carService = carService;
            _listingService = listingService;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCars();
            var listings = await _listingService.GetAllListings();

            var model = new AdminViewModel
            {
                Cars = cars.ToList(),
                Listings = listings.ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Listing(int id)
        {
            // if (id == null)
            // {
            //     return new BadRequestResult();
            // }

            var model = await _listingService.GetListingById(id);

            if (model != null)
            {
                var viewModel = _mapper.Map<ListingViewModel>(model);

                return View(viewModel);
            }

            return NotFound();
        }

        // [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditListing(ListingViewModel viewModel)
        {
            // TODO: Testing!
            var listing = await _listingService.GetListingById(viewModel.Id);

            if (listing == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (viewModel.FormFile != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "media");
                    uniqueFileName = Guid.NewGuid().ToString() + '_' + viewModel.FormFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    viewModel.FormFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                listing.Title = viewModel.Title;

                _listingService.UpdateListing(listing);

                return RedirectToAction("Listing", new { Id = viewModel.Id });
            }

            return View(viewModel);
        }

    }
}