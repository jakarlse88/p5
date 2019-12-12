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
            // Entity to ViewModel
            CreateMap<Listing, ListingViewModel>()
                .ForMember(
                    dest => dest.Media,
                    opt => opt.MapFrom(
                        (source, dest) => source.Media));
            
            CreateMap<Car, CarViewModel>();
            CreateMap<Media, MediaViewModel>();
            CreateMap<RepairJob, RepairJobViewModel>();
            
            // Entity to InputModel
            CreateMap<Listing, ListingInputModel>()
                .ForMember(
                    dest => dest.Files,
                    option => option.Ignore())
                .ForMember(
                    dest => dest.ImgNames,
                    option => option.Ignore())
                .ForMember(
                    dest => dest.Status,
                    option => option.MapFrom(
                        src => src.Status.Id));
            
            CreateMap<Car, CarInputModel>();
            
            CreateMap<Status, StatusInputModel>();
            CreateMap<RepairJob, RepairJobInputModel>()
                .ForMember(
                    dest => dest.Listing,
                    opt => opt.MapFrom(
                        src => src.Listing));
        }
    }
}