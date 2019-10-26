using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;

namespace TheCarHub.Repositories
{
    public interface ICarRepository
    {
        void UpdateCar(CarEntity car);
        void SaveCar(CarEntity car);
        void DeleteCar(int id);
        Task<CarEntity> GetCarById(int id);
        Task<IList<CarEntity>> GetAllCars();
    }
}