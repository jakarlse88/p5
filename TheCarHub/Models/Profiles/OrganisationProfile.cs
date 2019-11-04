using AutoMapper;
using TheCarHub.Models.Entities;
using TheCarHub.Models.ViewModels;

namespace TheCarHub.Models.Profiles
{
    public class OrganisationProfile : Profile 
    {
        public OrganisationProfile()
        {
            CreateMap<Listing, ListingViewModel>();
            CreateMap<Car, CarViewModel>();
        }
    }
}