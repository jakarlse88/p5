using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheCarHub.Areas.Admin.Controllers;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class TestAdminHomeController
    {
        [Fact]
        public async Task TestIndex()
        {
            // Arrange
            var mockListingService = new Mock<IListingService>();
            mockListingService
                .Setup(x => x.GetAllListingsAsViewModel())
                .ReturnsAsync(new List<ListingViewModel>())
                .Verifiable();

            var controller = new HomeController(mockListingService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(viewResult.Model);
        }

    }
}