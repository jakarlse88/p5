using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface ICarRepository
    {
        EntityEntry<Car> GetCarEntityEntry(Car car);
    }
}