using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;

namespace TheCarHub.Services
{
    public interface ICarService
    {
        Task<IList<Car>> GetAllCars();
    }
}