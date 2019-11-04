using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface ICarRepository
    {
        void Edit(Car car);
        void Add(Car car);
        void Delete(int id);
        Task<Car> GetById(int id);
        Task<IList<Car>> GetAll();
    }
}