using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
    public class FrontPageController : Controller
    {
        private readonly IListingService _listingService;

        public FrontPageController(IListingService listingService)
        {
            _listingService = listingService;
        }

        // GET: /
        public async Task<IActionResult> Index(string query)
        {
            ViewData["Query"] = query;

            if (string.IsNullOrWhiteSpace(query))
            {
                var viewModels = 
                    await _listingService
                        .GetAllListingsAsViewModel();
                
                return View(viewModels);
            }
            else
            {
                var viewModels =
                    await _listingService
                        .GetFilteredListingViewModels(
                            1,
                            query);
                
                return View(viewModels);
            }
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

            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}