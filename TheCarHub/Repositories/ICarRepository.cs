using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;

namespace TheCarHub.Repositories
{
    public interface ICarRepository
    {
        void UpdateCar(Car car);
        void SaveCar(Car car);
        void DeleteCar(int id);
        Task<Car> GetCarById(int id);
        Task<IList<Car>> GetAllCars();
    }
}