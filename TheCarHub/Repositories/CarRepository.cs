using TheCarHub.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories 
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public EntityEntry<Car> GetCarEntityEntry(Car entity)
        {
            if (entity == null) return null;

            var entry = _context.Entry(entity);

            return entry;
        }
    }
}