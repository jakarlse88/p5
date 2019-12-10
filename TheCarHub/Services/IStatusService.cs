using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services
{
    public interface IStatusService
    {
        Task<Status> GetStatusByIdAsync(int id);
    }
}