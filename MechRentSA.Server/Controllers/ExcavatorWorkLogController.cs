using Microsoft.AspNetCore.Mvc;
using MechRentSA.Server.Models;
using MechRentSA.Shared;
using Microsoft.EntityFrameworkCore;

namespace MechRentSA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcavatorWorkLogController : ControllerBase
    {
        private readonly DbMechRentSaContext _dbMechRentSaContext;

        public ExcavatorWorkLogController(DbMechRentSaContext dbMechRentSaContext)
        {
            _dbMechRentSaContext = dbMechRentSaContext;
        }

        [HttpGet]
        [Route("getExcavatorWorkLog")]
        public async Task<IActionResult> GetExcavatorWorkLog(string searchTerm = "", int pageNumber = 1, int pageSize = 10)
        {
            var responseApi = new ResponseAPI<PaginatedResponse<ExcavatorWorkLogDTO>>();
            var getExcavatorWorkLogDTO = new List<ExcavatorWorkLogDTO>();

            try
            {
                // Validación de parámetros de paginación
                if (pageNumber <= 0)
                    pageNumber = 1;
                if (pageSize <= 0)
                    pageSize = 10;

                // Obtener los registros con paginación y búsqueda
                var query = _dbMechRentSaContext.ExcavatorWorkLogs
                    .Include(pw => pw.PublicWork)
                    .Include(ex => ex.Excavator)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(e => e.PublicWork.Name.Contains(searchTerm) || e.Excavator.Type.Contains(searchTerm));
                }

                var totalItems = await query.CountAsync();

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(item => new ExcavatorWorkLogDTO
                    {
                        Id = item.Id,
                        PublicWorkId = item.PublicWorkId,
                        ExcavatorId = item.ExcavatorId,
                        HoursWorked = item.HoursWorked,
                        WorkDate = item.WorkDate,
                        ExcavatorDTO = new ExcavatorDTO
                        {
                            Id = item.Excavator.Id,
                            Type = item.Excavator.Type,
                            HourlyRate = item.Excavator.HourlyRate,
                            MaintenanceInterval = item.Excavator.MaintenanceInterval,
                            TotalHoursWorked = item.Excavator.TotalHoursWorked,
                            LastMaintenanceHours = item.Excavator.LastMaintenanceHours
                        },
                        PublicWorkDTO = new PublicWorkDTO
                        {
                            Id = item.PublicWork.Id,
                            Name = item.PublicWork.Name,
                            EstimatedHours = item.PublicWork.EstimatedHours
                        }
                    }).ToListAsync();

                var paginatedResponse = new PaginatedResponse<ExcavatorWorkLogDTO>
                {
                    Items = items,
                    TotalItems = totalItems,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                responseApi.IsSuccessful = true;
                responseApi.Value = paginatedResponse;
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost("addWorkHours")]
        public async Task<IActionResult> AddWorkHours([FromBody] AddWorkHoursDTO dto)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var excavatorWorkLog = new ExcavatorWorkLog
                {
                    PublicWorkId = dto.PublicWorkId,
                    ExcavatorId = dto.ExcavatorId,
                    HoursWorked = dto.HoursWorked,
                    WorkDate = DateTime.UtcNow
                };

                _dbMechRentSaContext.ExcavatorWorkLogs.Add(excavatorWorkLog);

                var excavator = await _dbMechRentSaContext.Excavators.FirstOrDefaultAsync(e => e.Id == dto.ExcavatorId);
                if (excavator != null)
                {
                    excavator.TotalHoursWorked += dto.HoursWorked;
                }

                await _dbMechRentSaContext.SaveChangesAsync();

                responseApi.IsSuccessful = true;
                responseApi.Value = excavatorWorkLog.Id;
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet("getExcavatorWorkValue/{publicWorkId}/{excavatorId}")]
        public async Task<IActionResult> GetExcavatorWorkValue(int publicWorkId, int excavatorId)
        {
            var responseApi = new ResponseAPI<decimal>();
            try
            {
                var workLogs = await _dbMechRentSaContext.ExcavatorWorkLogs
                    .Where(w => w.PublicWorkId == publicWorkId && w.ExcavatorId == excavatorId)
                    .Include(w => w.Excavator)
                    .ToListAsync();

                var totalValue = workLogs.Sum(w => w.HoursWorked * w.Excavator.HourlyRate);

                responseApi.IsSuccessful = true;
                responseApi.Value = totalValue;
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet("getExcavatorsNearMaintenance")]
        public async Task<IActionResult> GetExcavatorsNearMaintenance()
        {
            var responseApi = new ResponseAPI<List<ExcavatorDTO>>();
            try
            {
                var excavators = await _dbMechRentSaContext.Excavators
                    .Where(e => e.MaintenanceInterval - (e.TotalHoursWorked - e.LastMaintenanceHours) <= 120)
                    .Select(e => new ExcavatorDTO
                    {
                        Id = e.Id,
                        Type = e.Type,
                        HourlyRate = e.HourlyRate,
                        MaintenanceInterval = e.MaintenanceInterval,
                        TotalHoursWorked = e.TotalHoursWorked,
                        LastMaintenanceHours = e.LastMaintenanceHours
                    }).ToListAsync();

                responseApi.IsSuccessful = true;
                responseApi.Value = excavators;
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
