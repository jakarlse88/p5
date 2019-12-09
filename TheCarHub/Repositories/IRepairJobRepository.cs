using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface IRepairJobRepository
    {
        EntityEntry<RepairJob> GetRepairJobEntityEntry(RepairJob entity);
        void UpdateRepairJob(RepairJob repairJob);
        Task<RepairJob> GetRepairJobByIdAsync(int id);
    }
}