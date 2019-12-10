using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface IStatusRepository
    {
        Task<Status> GetStatusByIdAsync(int id);
    }
}