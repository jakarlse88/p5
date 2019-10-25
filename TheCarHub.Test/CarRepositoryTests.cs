using TheCarHub.Data;
using TheCarHub.Repositories;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace TheCarHub.Tests
{
    [Collection("DB")]
    public class CarRepositoryGetAllCarsTests
    {
        private readonly ApplicationDbContext _context;
        public CarRepositoryGetAllCarsTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("DataSource=app_ref.db")
                .Options;
            
            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async void TestGetAllCars()
        {
            // Arrange
            var repository = new CarRepository(_context);

            // Act
            var result = await repository.GetAllCars();

            // Assert
            Assert.NotEmpty(result);
        }
    }
}