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
        public async Task<IActionResult> GetExcavatorWorkLog(int pageNumber = 1, int pageSize = 10)
        {
            var responseApi = new ResponseAPI<List<ExcavatorWorkLogDTO>>();
            var getExcavatorWorkLogDTO = new List<ExcavatorWorkLogDTO>();

            try
            {
                // Validación de parámetros de paginación
                if (pageNumber <= 0)
                    pageNumber = 1;
                if (pageSize <= 0)
                    pageSize = 10;

                // Obtener los registros con paginación
                var query = _dbMechRentSaContext.ExcavatorWorkLogs
                    .Include(pw => pw.PublicWork)
                    .Include(ex => ex.Excavator)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(item => new ExcavatorWorkLogDTO
                    {
                        Id = item.Id,
                        PublicWorkId = item.PublicWorkId,
                        ExcavatorId = item.ExcavatorId,
                        HoursWorked = item.HoursWorked,
                        WorkDate = item.WorkDate,
                        excavatorDTO = new ExcavatorDTO
                        {
                            Id = item.Excavator.Id,
                            Type = item.Excavator.Type,
                            HourlyRate = item.Excavator.HourlyRate,
                            MaintenanceInterval = item.Excavator.MaintenanceInterval,
                            TotalHoursWorked = item.Excavator.TotalHoursWorked,
                            LastMaintenanceHours = item.Excavator.LastMaintenanceHours
                        },
                        publicWorkDTO = new PublicWorkDTO
                        {
                            Id = item.PublicWork.Id,
                            Name = item.PublicWork.Name,
                            EstimatedHours = item.PublicWork.EstimatedHours
                        }
                    });

                getExcavatorWorkLogDTO = await query.ToListAsync();

                responseApi.IsSuccessful = true;
                responseApi.Value = getExcavatorWorkLogDTO;
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
