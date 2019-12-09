using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Repositories;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;

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

        public async Task<Car> GetCarById(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);

            return car;
        }

        public void AddCar(Car car)
        {
            if (car != null)
            {
                _carRepository.AddCar(car);
            }
        }

        public void EditCar(Car car)
        {
            if (car != null)
            {
                _carRepository.UpdateCar(car);
            }
        }

        public void DeleteCar(int id)
        {
            _carRepository.DeleteCar(id);
        }

        public async Task UpdateCarExperimentalAsync(CarInputModel source)
        {
            var entity = await _carRepository.GetCarByIdAsync(source.Id);

            if (entity != null)
            {
                var entry = _carRepository.GetCarEntityEntry(entity);
                
                entry.CurrentValues.SetValues(source);
                
//                _carRepository.UpdateCar(entity);    
            }
        }
    }
}