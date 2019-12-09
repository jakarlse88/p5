using System.Threading.Tasks;
using TheCarHub.Models.InputModels;
using TheCarHub.Repositories;

namespace TheCarHub.Services
{
    public class RepairJobService : IRepairJobService
    {
        private readonly IRepairJobRepository _repairJobRepository;

        public RepairJobService(IRepairJobRepository repairJobRepository)
        {
            _repairJobRepository = repairJobRepository;
        }
        
        public async Task UpdateRepairJobAsync(RepairJobInputModel source)
        {
            var entity = 
                await _repairJobRepository.GetRepairJobByIdAsync(source.Id);

            if (entity != null)
            {
                var entry = _repairJobRepository.GetRepairJobEntityEntry(entity);
                
                entry.CurrentValues.SetValues(source);
                
//                _repairJobRepository.UpdateRepairJob(entity);
            }
        }
    }
}