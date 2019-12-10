using AutoMapper;
using TheCarHub.Models.Profiles;
using Xunit;

namespace TheCarHub.Test
{
    public class MappingTests
    {
        [Fact]
        public void TestAutoMapperProfiles()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile(new OrganisationProfile()));

            // Act

            // Assert
            config.AssertConfigurationIsValid();
        }
    }
}