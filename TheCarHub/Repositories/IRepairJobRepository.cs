using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface IRepairJobRepository
    {
        EntityEntry<RepairJob> GetRepairJobEntityEntry(RepairJob entity);
    }
}