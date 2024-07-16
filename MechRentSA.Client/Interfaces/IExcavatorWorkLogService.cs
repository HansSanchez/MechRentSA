using MechRentSA.Shared;

namespace MechRentSA.Client.Interfaces
{
    public interface IExcavatorWorkLogService
    {
        Task<PaginatedResponse<ExcavatorWorkLogDTO>> GetExcavatorWorkLogs(string searchTerm, int pageNumber, int pageSize);
        Task<int> AddWorkHours(AddWorkHoursDTO dto);
        Task<decimal> GetExcavatorWorkValue(int publicWorkId, int excavatorId);
        Task<List<ExcavatorDTO>> GetExcavatorsNearMaintenance();
    }
}
