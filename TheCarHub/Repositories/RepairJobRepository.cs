using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Data;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public class RepairJobRepository : IRepairJobRepository
    {
        private readonly ApplicationDbContext _context;

        public RepairJobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public EntityEntry<RepairJob> GetRepairJobEntityEntry(RepairJob entity)
        {
            if (entity == null) return null;

            var entry = _context.Entry(entity);

            return entry;
        }
    }
}