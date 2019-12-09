using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public void UpdateRepairJob(RepairJob repairJob)
        {
            if (repairJob != null)
            {
                _context.Update(repairJob);
                _context.SaveChanges();
            }
        }

        public Task<RepairJob> GetRepairJobByIdAsync(int id)
        {
            var repairJob =
                _context
                    .RepairJob
                    .FirstOrDefaultAsync(rj => rj.Id == id);

            return repairJob;
        }
    }
}