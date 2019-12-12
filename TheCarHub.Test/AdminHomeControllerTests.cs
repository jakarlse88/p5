using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheCarHub.Areas.Admin.Controllers;
using TheCarHub.Data;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class AdminHomeControllerTests
    {
        private DbContextOptions<ApplicationDbContext> DbContextOptions
        {
            get
            {
                var options =
                    new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

                return options;
            }
        }

        [Fact]
        public async Task TestIndex()
        {
            // Arrange
            var mockService = new Mock<IListingService>();
            mockService
                .Setup(x => x.GetAllListingsAsViewModel())
                .ReturnsAsync(new List<ListingViewModel>())
                .Verifiable();

            IActionResult result;

            var controller = new HomeController(mockService.Object);

            // Act
            result = await controller.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<List<ListingViewModel>>(viewResult.Model);

            mockService
                .Verify(x => x.GetAllListingsAsViewModel(), Times.Once);
        }
    }
}