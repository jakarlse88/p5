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
        
        public async Task<Status> GetStatusByIdAsync(int id)
        {
            var result =
                await _context
                    .Status
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync();
            
            return result;
        }
    }
}