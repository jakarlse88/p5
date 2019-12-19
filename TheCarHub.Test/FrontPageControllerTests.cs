using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheCarHub.Controllers;
using TheCarHub.Data;
using TheCarHub.Models.Profiles;
using TheCarHub.Models.ViewModels;
using TheCarHub.Repositories;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class FrontPageControllerTests
    {
        private static DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
        {
            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            return options;
        }

        private static MapperConfiguration GetMapperConfig()
        {
            return new MapperConfiguration(cfg =>
                cfg.AddProfile(new OrganisationProfile()));
        }

        [Fact]
        public async Task TestIndex()
        {
            // Arrange
            var options = BuildDbContextOptions();

            IActionResult result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var service = new ListingService(repository,
                    null,
                    null,
                    null,
                    null,
                    new Mapper(GetMapperConfig()));

                var controller = new FrontPageController(service);

                // Act
                result = await controller.Index(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(viewResult.Model);
        }

        [Fact]
        public async Task TestListingIdNull()
        {
            // Arrange
            var controller = new FrontPageController(null);

            // Act
            var result = await controller.Listing(null);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestListingValidId()
        {
            // Arrange
            var viewModel = new ListingViewModel{ Id = 1 };
            
            var mockService = new Mock<IListingService>();
            mockService
                .Setup(x => x.GetListingViewModelByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(viewModel)
                .Verifiable();

            var controller = new FrontPageController(mockService.Object);
            
            // Act
            var result = await controller.Listing(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<ListingViewModel>(viewResult.Model);

            mockService
                .Verify(x => x.GetListingViewModelByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public async Task TestListingInvalidId()
        {
            // Arrange
            ListingViewModel viewModel = null;
            
            var mockService = new Mock<IListingService>();
            mockService
                .Setup(x => x.GetListingViewModelByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(viewModel)
                .Verifiable();

            var controller = new FrontPageController(mockService.Object);
            
            // Act
            var result = await controller.Listing(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);

            mockService
                .Verify(x => x.GetListingViewModelByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task TestIndexQueryInvalid()
        {
            // Arrange
            var options = BuildDbContextOptions();

            IActionResult result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var service = new ListingService(repository,
                    null,
                    null,
                    null,
                    null,
                    new Mapper(GetMapperConfig()));

                var controller = new FrontPageController(service);

                // Act
                result = await controller.Index("test");

                context.Database.EnsureDeleted();
            }

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(viewResult.Model);
            Assert.Empty(modelResult);
        }

        [Fact]
        public async Task TestIndexQueryValid()
        {
            // Arrange
            var options = BuildDbContextOptions();

            IActionResult result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var service = new ListingService(repository,
                    null,
                    null,
                    null,
                    null,
                    new Mapper(GetMapperConfig()));

                var controller = new FrontPageController(service);

                // Act
                result = await controller.Index("ford");

                context.Database.EnsureDeleted();
            }

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(viewResult.Model);
            Assert.Equal(2, modelResult.Count());
        }

        [Fact]
        public void TestErrorStatusCode404()
        {
            // Arrange
            var controller = new FrontPageController(null);

            // Act
            var result = controller.Error(404);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.Equal("404", viewResult.ViewName);
        }
    }
}