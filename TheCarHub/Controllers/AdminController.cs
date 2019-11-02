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
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

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
        private readonly IConfiguration _configuration;

        public AdminController(ILogger<AdminController> logger,
            ICarService carService,
            IListingService listingService,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            IConfiguration configuration)
        {
            _logger = logger;
            _carService = carService;
            _listingService = listingService;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
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

            if (model == null) return NotFound();

            var viewModel = _mapper.Map<ListingViewModel>(model);

            return View(viewModel);
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
//                string uniqueFileName = null;

                if (viewModel.FormFile != null && viewModel.FormFile.Length > 0)
                {
//                    // Media directory path
//                    var mediaDir = Path.Combine(_hostEnvironment.WebRootPath, "media");
//
//                    // Don't trust user-supplied filenames!
//                    uniqueFileName = Guid.NewGuid().ToString() + '_' + viewModel.FormFile.FileName;


//                    var filePath = Path.Combine(mediaDir, uniqueFileName);
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(viewModel.FormFile.FileName);
//                    var filePath = Path.Combine(_configuration["Media:Directory"], fileName);
                    
                    var path = Path.Combine(_hostEnvironment.WebRootPath, _configuration["Media:Directory"], $"{fileName}");
                    
                    // Add filepath to the Listing's list of Media objects
                    if (listing.Media == null)
                    {
                        listing.Media = new List<Media>();
                    }

                    listing.Media.Add(new Media
                    {
                        FileName = fileName,
                        ListingId = viewModel.Id,
                        Listing = listing
                        // TODO: Caption, MediaTags
                    });

                    // Copy file to media directory
                    

                    using (var stream = System.IO.File.Create(path))
                    {
                        await viewModel.FormFile.CopyToAsync(stream);
                    }
//                    viewModel.FormFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                listing.Title = viewModel.Title;

                _listingService.UpdateListing(listing);

                return RedirectToAction("Listing", new {Id = viewModel.Id});
            }

            return View(viewModel);
        }
    }
}