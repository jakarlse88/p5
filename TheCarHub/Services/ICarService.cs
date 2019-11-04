using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services
{
    public interface ICarService
    {
        Task<IList<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        void Add(Car car);
        void Edit(Car car);
        void Delete(int id);
    }
}