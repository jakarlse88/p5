using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;

namespace TheCarHub.Repositories
{
    public interface ICarRepository
    {
        void Save(CarEntity car);
        Task<CarEntity> GetCarById(int id);
        Task<IList<CarEntity>> GetAllCars();
    }
}