using AutoMapper;
using TheCarHub.Models.Profiles;
using Xunit;

namespace TheCarHub.Test
{
    public class MappingTests
    {
        [Fact]
        public void TestMapListingToListingViewModel()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile(new OrganisationProfile()));

            // Act

            // Assert
            config.AssertConfigurationIsValid();
        }

//        [Fact]
//        public void TestMapListingToListingInputModel()
//        {
//            // Arrange
//            var config = new MapperConfiguration(cfg =>
//                cfg.CreateMap<Listing, ListingInputModel>()
//                    
//                );
//            
//            // Act
//
//            // Assert
//            config.AssertConfigurationIsValid();
//        }


    }
}