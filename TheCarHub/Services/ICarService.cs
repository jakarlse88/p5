using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services
{
    public interface ICarService
    {
        Task<IList<Car>> GetAllCars();
    }
}