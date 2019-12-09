using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Services
{
    public interface IRepairJobService
    {
        Task UpdateRepairJobAsync(RepairJobInputModel source);
//        Task<RepairJob> GetRepairJobByIdAsync(int id);
    }
}