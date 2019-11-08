using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface ICarRepository
    {
        void EditCar(Car car);
        void AddCar(Car car);
        void DeleteCar(int id);
        Task<Car> GetCarById(int id);
        Task<IList<Car>> GetAllCars();
    }
}