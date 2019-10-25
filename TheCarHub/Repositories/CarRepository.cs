using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
using TheCarHub.Data;
using Microsoft.EntityFrameworkCore;

namespace TheCarHub.Repositories 
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CarItem>> GetAllCars()
        {
            var results = await _context.Cars.ToListAsync();

            return results;
        }

        public Task<CarItem> GetCarById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(CarItem car)
        {
            throw new NotImplementedException();
        }
    }
}