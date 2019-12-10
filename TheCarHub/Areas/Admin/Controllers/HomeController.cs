using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCarHub.Services;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IListingService _listingService;

        public HomeController(IListingService listingService)
        {
            _listingService = listingService;
        }
        
        // GET: Admin/
        public async Task<IActionResult> Index()
        {
            var viewModels = await _listingService.GetAllListingsAsViewModel();           

            return View(viewModels.ToList());
        }
    }
}