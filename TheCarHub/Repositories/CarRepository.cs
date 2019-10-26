using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
using TheCarHub.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TheCarHub.Repositories 
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CarEntity>> GetAllCars()
        {
            var results = await _context.Cars.ToListAsync();

            return results;
        }

        public async Task<CarEntity> GetCarById(int id)
        {
            CarEntity result = 
                await _context
                    .Cars
                    .FirstOrDefaultAsync(x => x.CarId == id);
            
            return result;
        }

        public void DeleteCar(int id)
        {
            CarEntity car = 
                _context
                .Cars
                .FirstOrDefault(c => c.CarId == id);

            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }

        public void UpdateCar(CarEntity car)
        {
            if (car != null)
            {
                _context.Cars.Update(car);
                _context.SaveChanges();
            }
        }

        public void SaveCar(CarEntity car)
        {
            if (car != null)
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
            }
        }
    }
}