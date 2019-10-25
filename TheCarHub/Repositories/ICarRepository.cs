using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;

namespace TheCarHub.Repositories
{
    public interface ICarRepository
    {
        void Save(CarItem car);
        Task<CarItem> GetCarById(Guid id);
        Task<IList<CarItem>> GetAllCars();
    }
}