using System.Collections.Generic;
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
        
        public async Task<ICollection<Status>> GetAllStatuses()
        {
            var result = await _statusRepository.GetAllStatuses();

            return result;
        }

        public async Task<Status> GetStatusByNameAsync(string statusName = "")
        {
            var result = 
                await _statusRepository.GetStatusByNameAsync(statusName);

            return result;
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            var result = await _statusRepository.GetStatusByIdAsync(id);

            return result;
        }

        public Status GetStatusById(int id)
        {
            var result = _statusRepository.GetStatusById(id);

            return result;
        }
    }
}