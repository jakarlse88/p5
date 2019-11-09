//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using TheCarHub.Models;
//using TheCarHub.Services;
//using System.Linq;
//using System;
//using AutoMapper;
//using Microsoft.AspNetCore.Hosting;
//using System.IO;
//using System.Collections.Generic;
//using Microsoft.Extensions.Configuration;
//using TheCarHub.Models.Entities;
//using TheCarHub.Models.ViewModels;
//
//namespace TheCarHub.Controllers
//{
//    [Authorize]
//    public class __AdminController : Controller
//    {
//        private readonly ILogger<__AdminController> _logger;
//        private readonly ICarService _carService;
//        private readonly IListingService _listingService;
//        private readonly IMapper _mapper;
//        private readonly IWebHostEnvironment _hostEnvironment;
//        private readonly IConfiguration _configuration;
//
//        public __AdminController(ILogger<__AdminController> logger,
//            ICarService carService,
//            IListingService listingService,
//            IMapper mapper,
//            IWebHostEnvironment hostEnvironment,
//            IConfiguration configuration)
//        {
//            _logger = logger;
//            _carService = carService;
//            _listingService = listingService;
//            _mapper = mapper;
//            _hostEnvironment = hostEnvironment;
//            _configuration = configuration;
//        }
//
//        // GET: Admin
//        public async Task<IActionResult> Index()
//        {
//            var cars = await _carService.GetAllCars();
//            var listings = await _listingService.GetAll();
//
//            var model = new AdminViewModel
//            {
//                Cars = cars.ToList(),
//                Listings = listings.ToList()
//            };
//
//            return View(model);
//        }
//
//        public async Task<IActionResult> Listing(int id)
//        {
//            // if (id == null)
//            // {
//            //     return new BadRequestResult();
//            // }
//
//            var model = await _listingService.GetById(id);
//
//            if (model == null) return NotFound();
//
//            var viewModel = _mapper.Map<ListingViewModel>(model);
//
//            return View(viewModel);
//        }
//
//        // [ValidateAntiForgeryToken]
//        [HttpPost]
//        [ActionName("Edit")]
//        public async Task<IActionResult> EditListing(ListingViewModel viewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                // TODO: Testing!
//                var listing = await _listingService.GetById(viewModel.Id);
//
//                if (listing == null)
//                {
//                    return NotFound();
//                }
//
//                // Image upload
//                if (viewModel.FormFiles != null && viewModel.FormFiles.Count > 0)
//                {
//                    foreach (var file in viewModel.FormFiles)
//                    {
//                        if (file.Length > 0)
//                        {
//                            var fileName =
//                                Path.GetRandomFileName() + Path.GetExtension(file.FileName);
//
//                            var path =
//                                Path.Combine(_hostEnvironment.WebRootPath,
//                                    _configuration["Media:Directory"], $"{fileName}");
//
//                            if (listing.Media == null)
//                            {
//                                listing.Media = new List<Media>();
//                            }
//
//                            listing.Media.Add(new Media
//                            {
//                                FileName = fileName,
//                                ListingId = listing.Id,
//                                Listing = listing,
//                                Caption = "",
//                                MediaTags = new List<MediaTag>()
//                            });
//
//                            using (var stream = System.IO.File.Create(path))
//                            {
//                                await file.CopyToAsync(stream);
//                            }
//                        }
//                    }
//                }
//
//                listing.Title = viewModel.Title;
//                listing.DateLastUpdated = DateTime.Today;
//                listing.Status = viewModel.Status;
//
//                _listingService.Edit(listing);
//
//                return RedirectToAction("Listing", new {Id = viewModel.Id});
//            }
//
//            return View("Listing", viewModel);
//        }
//    }
//}
