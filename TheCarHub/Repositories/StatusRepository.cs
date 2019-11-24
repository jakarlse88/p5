using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<ICollection<Status>> GetAllStatuses()
        {
            var result = await _context.Status.ToListAsync();

            return result;
        }

        public async Task<Status> GetStatusByName(string statusName = "")
        {
            var result =
                await _context
                    .Status
                    .Where(i => i.Name.Contains(statusName.ToLower()))
                    .FirstAsync();
            
            return result;
        }
    }
}