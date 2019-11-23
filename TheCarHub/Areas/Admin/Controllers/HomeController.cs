using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCarHub.Models.Entities;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        private readonly IListingService _listingService;
        private readonly IMapper _mapper;

        public HomeController(ICarService carService,
            IListingService listingService,
            IMapper mapper)
        {
            _carService = carService;
            _listingService = listingService;
            _mapper = mapper;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var listings =
                await _listingService
                    .GetAllListings();

            var viewModels = 
                _mapper.Map<List<Listing>, List<ListingViewModel>>(listings.ToList());

            return View(viewModels);
        }
    }
}