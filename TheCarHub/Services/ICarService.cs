using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Services
{
    public interface ICarService
    {
        void MapCarValues(CarInputModel carInputModel, Car carEntity);
    }
}