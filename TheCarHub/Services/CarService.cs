using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
using TheCarHub.Repositories;
using System.Linq;

namespace TheCarHub.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IList<Car>> GetAllCars()
        {
            var cars = await _carRepository.GetAllCars();

            return cars;
        }
    }
}