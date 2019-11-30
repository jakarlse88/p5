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

        public async Task<Status> GetStatusByNameAsync(string statusName = "")
        {
            var result =
                await _context
                    .Status
                    .Where(s => s.Name.ToLower() == statusName.ToLower())
                    .FirstOrDefaultAsync();
            
            return result;
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            var result =
                await _context
                    .Status
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync();
            
            return result;
        }

        public Status GetStatusById(int id)
        {
            var result =
                _context.Status.FirstOrDefault(s => s.Id == id); 

            return result;
        }
    }
}