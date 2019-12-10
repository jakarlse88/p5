using System;
using TheCarHub.Models.Entities;
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
        
        public void MapRepairJobValues(RepairJobInputModel repairJobInputModel, RepairJob repairJobEntity)
        {
            if (repairJobInputModel == null)
            {
                throw new Exception("InputModel cannot be null.");
            }
            
            if (repairJobEntity == null)
            {
                throw new Exception("RepairJob entity not found.");
            }
            
            
            var repairJobEntityEntry = _repairJobRepository.GetRepairJobEntityEntry(repairJobEntity);
            
            repairJobEntityEntry.CurrentValues.SetValues(repairJobInputModel);
        }
    }
}