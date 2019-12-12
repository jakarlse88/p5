using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Models.Profiles;
using TheCarHub.Models.ViewModels;
using TheCarHub.Repositories;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class ListingServiceTests
    {
        private readonly IMapper _mapper;

        public ListingServiceTests()
        {
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile(new OrganisationProfile()));
            
            _mapper = new Mapper(config);
        }
        
        private DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
        {
            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            return options;
        }

        [Fact]
        public async Task TestGetAllListingsAsync()
        {
            // Arrange
            var options = BuildDbContextOptions();
            IEnumerable<Listing> result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var service = new ListingService(repository, null, null, null, null, null);

                result = await service.GetAllListings();
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(
                    context.Listing.ToList().Count, result.Count());

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void TestGetListingById()
        {
            // Arrange
            var options = BuildDbContextOptions();
            Listing result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var service = new ListingService(repository, null, null, null, null, null);

                result = await service.GetListingByIdAsync(1);
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, result.Id);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestAddNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            ListingInputModel testObject = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var service = new ListingService(repository, null, null, null, null, null);

                service.AddListingAsync(testObject);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.ToList();

                Assert.Equal(6, result.Count);

                context.Database.EnsureDeleted();
            }
        }


        [Fact]
        public void TestUpdateListingExperimentalAsyncArgumentNull()
        {
            // Arrange
            var mockRepository = new Mock<IListingRepository>();
            mockRepository
                .Setup(x => x.GetListingById(It.IsAny<int>()))
                .Verifiable();

            var service = new ListingService(
                mockRepository.Object,
                null,
                null,
                null,
                null,
                null);

            // Act
            service.UpdateListingAsync(null);

            // Assert
            mockRepository
                .Verify(x => x.GetListingById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void TestUpdateListingExperimentalAsyncSourceInvalidId()
        {
            // Arrange
            var mockRepository = new Mock<IListingRepository>();
            mockRepository
                .Setup(x => x.GetListingById(It.IsAny<int>()))
                .Verifiable();

            var service = new ListingService(
                mockRepository.Object,
                null,
                null,
                null,
                null,
                null);

            // Act
            service.UpdateListingAsync(null);

            // Assert
            mockRepository
                .Verify(x => x.GetListingById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task TestGetAllListingsAsViewModel()
        {
            // Arrange
            var options = BuildDbContextOptions();

            IEnumerable<ListingViewModel> result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetAllListingsAsViewModel();
            }

            await using (var context = new ApplicationDbContext(options))
            {
                // Assert
                var enumerable = result.ToList();
                
                Assert.NotNull(result);
                Assert.NotEmpty(enumerable);
                Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(result);
                Assert.Equal(6, enumerable.Count());

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetAllListingsAsViewModelEmptyContext()
        {
            // Arrange
            var options = BuildDbContextOptions();

            IEnumerable<ListingViewModel> result;

            await using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetAllListingsAsViewModel();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task TestGetFilteredListingViewModelsBadQuery(string query)
        {
            // Arrange
            var options = BuildDbContextOptions();

            IEnumerable<ListingViewModel> result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetFilteredListingViewModels(1, query);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(result);
            Assert.Equal(2, result.Count());
        }

        [Theory]
        [InlineData("Dacia")]
        [InlineData("Daewoo")]
        [InlineData("Citroën")]
        public async Task TestGetFilteredListingViewModelsInvalidQuery(string query)
        {
            // Arrange
            var options = BuildDbContextOptions();

            IEnumerable<ListingViewModel> result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetFilteredListingViewModels(1, query);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(result);
        }

        [Fact]
        public async Task TestGetFilteredListingViewModelsValidQuery()
        {
            // Arrange
            var options = BuildDbContextOptions();

            IEnumerable<ListingViewModel> result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetFilteredListingViewModels(1, "Ford");

                context.Database.EnsureDeleted();
            }
            
            // Assert
            var enumerable = result.ToList();
            
            Assert.NotEmpty(enumerable);
            Assert.IsAssignableFrom<IEnumerable<ListingViewModel>>(result);
            Assert.Single(enumerable);
            Assert.Equal("Ford", enumerable.First().Car.Make);
            Assert.Equal(1, enumerable.First().Status.Id);
        }

        [Fact]
        public async Task TestGetListingInputModelByIdAsyncInvalidId()
        {
            // Arrange
            var options = BuildDbContextOptions();

            ListingInputModel result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetListingInputModelByIdAsync(666);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TestGetListingInputModelByIdAsyncIdValid()
        {
            // Arrange
            var options = BuildDbContextOptions();

            ListingInputModel result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetListingInputModelByIdAsync(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ListingInputModel>(result);
            Assert.Equal("one description", result.Description);
        }

        [Fact]
        public async Task TestGetListingViewModelByIdAsyncInvalidId()
        {
            // Arrange
            var options = BuildDbContextOptions();

            ListingViewModel result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetListingViewModelByIdAsync(666);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TestGetListingViewModelByIdAsyncIdValid()
        {
            // Arrange
            var options = BuildDbContextOptions();

            ListingViewModel result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    _mapper);
            
                // Act
                result = await service.GetListingViewModelByIdAsync(1);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ListingViewModel>(result);
            Assert.Equal("one description", result.Description);
        }

        [Fact]
        public async Task TestAddListingAsyncInputModelNull()
        {
            // Arrange
            var options = BuildDbContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    null,
                    null,
                    null,
                    null, 
                    null);
                
                Assert.Equal(6, context.Listing.Count());
                
                // Act
                await service.AddListingAsync(null);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                // Assert
                Assert.Equal(6, context.Listing.Count());

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestAddListingAsyncInputModelValid()
        {
            // Arrange
            var options = BuildDbContextOptions();

            var newListing = new ListingInputModel 
            {
                Description = "new listing"
            };

            var mockRepairJobService = new Mock<IRepairJobService>();
            mockRepairJobService
                .Setup(x => x.MapRepairJobValues(
                    It.IsAny<RepairJobInputModel>(),
                    It.IsAny<RepairJob>()))
                .Verifiable();

            var mockCarService = new Mock<ICarService>();
            mockCarService
                .Setup(x => x.MapCarValues(
                    It.IsAny<CarInputModel>(),
                    It.IsAny<Car>()))
                .Verifiable();

            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.UpdateMediaCollection(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<Listing>()))
                .Verifiable();
            
            var mockStatusService = new Mock<IStatusService>();
            
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var status = context.Status.FirstOrDefault(s => s.Id == 1);
                
                mockStatusService
                    .Setup(x => x.GetStatusByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(status)
                    .Verifiable();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    mockRepairJobService.Object, 
                    mockCarService.Object, 
                    mockStatusService.Object, 
                    mockMediaService.Object, 
                    _mapper);
                
                Assert.Equal(6, context.Listing.Count());
                
                // Act
                await service.AddListingAsync(newListing);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                // Assert
                var listings = context.Listing.ToList();
                
                Assert.Equal(7, listings.Count);
                Assert.Contains(listings, l => l.Description == "new listing");
                
                mockRepairJobService
                    .Verify(x => x.MapRepairJobValues(
                        It.IsAny<RepairJobInputModel>(),
                        It.IsAny<RepairJob>()),
                        Times.Once);

                mockCarService
                    .Verify(x => x.MapCarValues(
                            It.IsAny<CarInputModel>(),
                            It.IsAny<Car>()),
                        Times.Once);

                mockStatusService
                    .Verify(x => x.GetStatusByIdAsync(It.IsAny<int>()), 
                        Times.Once);

                mockMediaService
                    .Verify(x => x.UpdateMediaCollection(
                            It.IsAny<IEnumerable<string>>(),
                            It.IsAny<Listing>()),
                        Times.Once);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestUpdateListingAsyncSourceNull()
        {
            // Arrange
            var options = BuildDbContextOptions();

            ListingInputModel testInputModel = null; 

            var mockRepairJobService = new Mock<IRepairJobService>();
            mockRepairJobService
                .Setup(x => x.MapRepairJobValues(
                    It.IsAny<RepairJobInputModel>(),
                    It.IsAny<RepairJob>()))
                .Verifiable();

            var mockCarService = new Mock<ICarService>();
            mockCarService
                .Setup(x => x.MapCarValues(
                    It.IsAny<CarInputModel>(),
                    It.IsAny<Car>()))
                .Verifiable();

            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.UpdateMediaCollection(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<Listing>()))
                .Verifiable();
            
            var mockStatusService = new Mock<IStatusService>();
            
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var status = context.Status.FirstOrDefault(s => s.Id == 1);
                
                mockStatusService
                    .Setup(x => x.GetStatusByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(status)
                    .Verifiable();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    mockRepairJobService.Object, 
                    mockCarService.Object, 
                    mockStatusService.Object, 
                    mockMediaService.Object, 
                    _mapper);
                
                Assert.Equal(6, context.Listing.Count());
                
                // Act
                await service.AddListingAsync(testInputModel);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                // Assert
                var listings = context.Listing.ToList();
                
                Assert.Equal(6, listings.Count);
                
                mockRepairJobService
                    .Verify(x => x.MapRepairJobValues(
                        It.IsAny<RepairJobInputModel>(),
                        It.IsAny<RepairJob>()),
                        Times.Never);

                mockCarService
                    .Verify(x => x.MapCarValues(
                            It.IsAny<CarInputModel>(),
                            It.IsAny<Car>()),
                        Times.Never);

                mockStatusService
                    .Verify(x => x.GetStatusByIdAsync(It.IsAny<int>()), 
                        Times.Never);

                mockMediaService
                    .Verify(x => x.UpdateMediaCollection(
                            It.IsAny<IEnumerable<string>>(),
                            It.IsAny<Listing>()),
                        Times.Never);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestUpdateListingAsyncSourceValid()
        {
            // Arrange
            var options = BuildDbContextOptions();

            var inputModel = new ListingInputModel 
            {
                Id = 1,
                Description = "updated listing"
            };

            var mockRepairJobService = new Mock<IRepairJobService>();
            mockRepairJobService
                .Setup(x => x.MapRepairJobValues(
                    It.IsAny<RepairJobInputModel>(),
                    It.IsAny<RepairJob>()))
                .Verifiable();

            var mockCarService = new Mock<ICarService>();
            mockCarService
                .Setup(x => x.MapCarValues(
                    It.IsAny<CarInputModel>(),
                    It.IsAny<Car>()))
                .Verifiable();

            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.UpdateMediaCollection(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<Listing>()))
                .Verifiable();
            
            var mockStatusService = new Mock<IStatusService>();
            
            bool result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var status = context.Status.FirstOrDefault(s => s.Id == 1);
                
                mockStatusService
                    .Setup(x => x.GetStatusByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(status)
                    .Verifiable();
                
                var repository = new ListingRepository(context);
                
                var service = new ListingService(repository, 
                    mockRepairJobService.Object, 
                    mockCarService.Object, 
                    mockStatusService.Object, 
                    mockMediaService.Object, 
                    _mapper);
                
                // Act
                result = await service.UpdateListingAsync(inputModel);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                // Assert
                var listings = context.Listing.ToList();
                
                Assert.True(result);
                Assert.Equal("updated listing", listings.FirstOrDefault(l => l.Id == 1).Description);
                
                mockRepairJobService
                    .Verify(x => x.MapRepairJobValues(
                        It.IsAny<RepairJobInputModel>(),
                        It.IsAny<RepairJob>()),
                        Times.Once);

                mockCarService
                    .Verify(x => x.MapCarValues(
                            It.IsAny<CarInputModel>(),
                            It.IsAny<Car>()),
                        Times.Once);

                mockStatusService
                    .Verify(x => x.GetStatusByIdAsync(It.IsAny<int>()), 
                        Times.Once);

                mockMediaService
                    .Verify(x => x.UpdateMediaCollection(
                            It.IsAny<IEnumerable<string>>(),
                            It.IsAny<Listing>()),
                        Times.Once);

                context.Database.EnsureDeleted();
            }
        }

    }
}