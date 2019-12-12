using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;

namespace TheCarHub.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            var result = await _statusRepository.GetStatusByIdAsync(id);

            return result;
        }
    }
}