using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface ICarRepository
    {
        void UpdateCar(Car car);
        void AddCar(Car car);
        void DeleteCar(int id);
        Task<Car> GetCarByIdAsync(int id);
        Task<IList<Car>> GetAllCars();
        EntityEntry<Car> GetCarEntityEntry(Car car);
    }
}