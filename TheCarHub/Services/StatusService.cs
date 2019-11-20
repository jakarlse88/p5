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

        public async Task<Status> GetStatusByName(string statusName = "")
        {
            var result = await _statusRepository.GetStatusByName(statusName);

            return result;
        }
    }
}