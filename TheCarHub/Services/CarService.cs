using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Repositories;
using TheCarHub.Models.Entities;

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
            var cars = await _carRepository.GetAll();

            return cars;
        }

        public async Task<Car> GetCarById(int id)
        {
            var car = await _carRepository.GetById(id);

            return car;
        }

        public void Add(Car car)
        {
            if (car != null)
            {
                _carRepository.Add(car);
            }
        }

        public void Edit(Car car)
        {
            if (car != null)
            {
                _carRepository.Edit(car);
            }
        }

        public void Delete(int id)
        {
            _carRepository.Delete(id);
        }
    }
}