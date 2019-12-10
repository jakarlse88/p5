using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Services
{
    public interface IRepairJobService
    {
        void MapRepairJobValues(RepairJobInputModel repairJobInputModel, RepairJob repairJobEntity);
    }
}