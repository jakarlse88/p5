using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<IList<Car>> GetAllCars()
        {
            var results = await _context.Car.ToListAsync();

            return results;
        }

        public async Task<Car> GetCarById(int id)
        {
            var result = 
                await _context
                    .Car
                    .FirstOrDefaultAsync(x => x.Id == id);
            
            return result;
        }

        public void DeleteCar(int id)
        {
            var car = 
                _context
                .Car
                .FirstOrDefault(c => c.Id == id);

            if (car != null)
            {
                _context.Car.Remove(car);
                _context.SaveChanges();
            }
        }

        public void EditCar(Car car)
        {
            if (car != null)
            {
                _context.Car.Update(car);
                _context.SaveChanges();
            }
        }

        public void AddCar(Car car)
        {
            if (car != null)
            {
                _context.Car.Add(car);
                _context.SaveChanges();
            }
        }
    }
}