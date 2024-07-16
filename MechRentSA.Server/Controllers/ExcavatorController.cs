using Microsoft.AspNetCore.Mvc;
using MechRentSA.Server.Models;
using MechRentSA.Shared;
using Microsoft.EntityFrameworkCore;

namespace MechRentSA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcavatorController : ControllerBase
    {
        private readonly DbMechRentSaContext _dbMechRentSaContext;

        public ExcavatorController(DbMechRentSaContext dbMechRentSaContext)
        {
            _dbMechRentSaContext = dbMechRentSaContext;
        }

        [HttpGet("getExcavator")]
        public async Task<IActionResult> GetExcavators(string searchTerm = "", int pageNumber = 1, int pageSize = 2)
        {
            var responseApi = new ResponseAPI<PaginatedResponse<ExcavatorDTO>>();
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 2;

                var query = _dbMechRentSaContext.Excavators.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(e => e.Type.Contains(searchTerm));
                }

                var totalItems = await query.CountAsync();

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(item => new ExcavatorDTO
                    {
                        Id = item.Id,
                        Type = item.Type,
                        HourlyRate = item.HourlyRate,
                        MaintenanceInterval = item.MaintenanceInterval,
                        TotalHoursWorked = item.TotalHoursWorked,
                        LastMaintenanceHours = item.LastMaintenanceHours
                    }).ToListAsync();

                var paginatedResponse = new PaginatedResponse<ExcavatorDTO>
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

        [HttpGet("getByIdExcavator/{id}")]
        public async Task<IActionResult> GetByIdExcavator(int id)
        {
            var responseApi = new ResponseAPI<ExcavatorDTO>();
            try
            {
                var dbExcavator = await _dbMechRentSaContext.Excavators.FirstOrDefaultAsync(x => x.Id == id);

                if (dbExcavator != null)
                {
                    var getExcavatorDTO = new ExcavatorDTO
                    {
                        Id = dbExcavator.Id,
                        Type = dbExcavator.Type,
                        HourlyRate = dbExcavator.HourlyRate,
                        MaintenanceInterval = dbExcavator.MaintenanceInterval,
                        TotalHoursWorked = dbExcavator.TotalHoursWorked,
                        LastMaintenanceHours = dbExcavator.LastMaintenanceHours
                    };

                    responseApi.IsSuccessful = true;
                    responseApi.Value = getExcavatorDTO;
                }
                else
                {
                    responseApi.IsSuccessful = false;
                    responseApi.Message = "No se ha encontrado la excavadora que buscas por Id";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost("storeExcavator")]
        public async Task<IActionResult> StoreExcavator(ExcavatorDTO excavatorDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbExcavator = new Excavator
                {
                    Type = excavatorDTO.Type,
                    HourlyRate = excavatorDTO.HourlyRate,
                    MaintenanceInterval = excavatorDTO.MaintenanceInterval,
                    TotalHoursWorked = excavatorDTO.TotalHoursWorked,
                    LastMaintenanceHours = excavatorDTO.LastMaintenanceHours
                };

                _dbMechRentSaContext.Add(dbExcavator);
                await _dbMechRentSaContext.SaveChangesAsync();

                responseApi.IsSuccessful = true;
                responseApi.Value = dbExcavator.Id;
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut("updateExcavator/{id}")]
        public async Task<IActionResult> UpdateExcavator(int id, ExcavatorDTO excavatorDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbExcavator = await _dbMechRentSaContext.Excavators.FirstOrDefaultAsync(x => x.Id == id);

                if (dbExcavator != null)
                {
                    dbExcavator.Type = excavatorDTO.Type;
                    dbExcavator.HourlyRate = excavatorDTO.HourlyRate;
                    dbExcavator.MaintenanceInterval = excavatorDTO.MaintenanceInterval;
                    dbExcavator.TotalHoursWorked = excavatorDTO.TotalHoursWorked;
                    dbExcavator.LastMaintenanceHours = excavatorDTO.LastMaintenanceHours;

                    _dbMechRentSaContext.Update(dbExcavator);
                    await _dbMechRentSaContext.SaveChangesAsync();

                    responseApi.IsSuccessful = true;
                    responseApi.Value = dbExcavator.Id;
                }
                else
                {
                    responseApi.IsSuccessful = false;
                    responseApi.Message = "No ha sido posible encontrar la excavadora con ese Id";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpDelete("deleteExcavator/{id}")]
        public async Task<IActionResult> DeleteExcavator(int id)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbExcavator = await _dbMechRentSaContext.Excavators
                    .Include(e => e.ExcavatorWorkLogs)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (dbExcavator != null)
                {
                    if (dbExcavator.ExcavatorWorkLogs.Any())
                    {
                        responseApi.IsSuccessful = false;
                        responseApi.Message = "No se puede eliminar la excavadora porque tiene registros de trabajo relacionados.";
                    }
                    else
                    {
                        _dbMechRentSaContext.Excavators.Remove(dbExcavator);
                        await _dbMechRentSaContext.SaveChangesAsync();

                        responseApi.IsSuccessful = true;
                        responseApi.Value = dbExcavator.Id;
                    }
                }
                else
                {
                    responseApi.IsSuccessful = false;
                    responseApi.Message = "No ha sido posible encontrar la excavadora con ese Id";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.InnerException?.Message ?? ex.Message;
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
