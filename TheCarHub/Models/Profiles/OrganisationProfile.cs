using AutoMapper;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Models.ViewModels;

namespace TheCarHub.Models.Profiles
{
    public class OrganisationProfile : Profile
    {
        public OrganisationProfile()
        {
            CreateMap<Listing, ListingViewModel>();
            CreateMap<Listing, ListingInputModel>()
                .ForMember(
                    dest => dest.Files,
                    option => option.Ignore())
                .ForMember(
                    dest => dest.ImgNames,
                    option => option.Ignore())
                .ForMember(
                    dest => dest.CarYear,
                    opt => opt.MapFrom<int>(
                        (source, dest) => source.Car.Year.Year));

            CreateMap<Car, CarViewModel>();
            CreateMap<Car, CarInputModel>();

            CreateMap<Media, MediaViewModel>();

            CreateMap<RepairJob, RepairJobViewModel>();
            CreateMap<RepairJob, RepairJobInputModel>();
        }
    }
}