using AutoMapper;

namespace TheCarHub.Models
{
    public class OrganisationProfile : Profile 
    {
        public OrganisationProfile()
        {
            CreateMap<Listing, ListingViewModel>();
        }
    }
}