using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Services
{
    public interface ICarService
    {
        Task<IList<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        void AddCar(Car car);
        void EditCar(Car car);
        void DeleteCar(int id);
        Task UpdateCarExperimentalAsync(CarInputModel source);
    }
}