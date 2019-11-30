using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services
{
    public interface IStatusService
    {
        Task<ICollection<Status>> GetAllStatuses();
        Task<Status> GetStatusByNameAsync(string statusName);
        Task<Status> GetStatusByIdAsync(int id);
        Status GetStatusById(int id);
    }
}