using System;
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

        public void MapCarValues(CarInputModel carInputModel, Car carEntity)
        {
            if (carInputModel == null)
            {
                throw new Exception("InputModel argument cannot be null.");
            }
            
            if (carEntity == null)
            {
                throw new Exception("Car entity not found.");
            }
            
            var carEntityEntry = _carRepository.GetCarEntityEntry(carEntity);
            
            carEntityEntry.CurrentValues.SetValues(carInputModel);
        }
    }
}