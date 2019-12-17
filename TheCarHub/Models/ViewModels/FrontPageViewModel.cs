using System.Collections.Generic;

namespace TheCarHub.Models.ViewModels
{
    public class FrontPageViewModel
    {
        public IEnumerable<ListingViewModel> ListingViewModels { get; set; }
        public string SearchString { get; set; }
    }
}