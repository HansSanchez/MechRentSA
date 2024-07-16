using Microsoft.AspNetCore.Mvc;
using MechRentSA.Server.Models;
using MechRentSA.Shared;
using Microsoft.EntityFrameworkCore;

namespace MechRentSA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicWorkController : ControllerBase
    {
        private readonly DbMechRentSaContext _dbMechRentSaContext;

        public PublicWorkController(DbMechRentSaContext dbMechRentSaContext)
        {
            _dbMechRentSaContext = dbMechRentSaContext;
        }

        [HttpGet("getPublicWork")]
        public async Task<IActionResult> GetPublicWorks(string searchTerm = "", int pageNumber = 1, int pageSize = 2)
        {
            var responseApi = new ResponseAPI<PaginatedResponse<PublicWorkDTO>>();
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 2;

                var query = _dbMechRentSaContext.PublicWorks.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(e => e.Name.Contains(searchTerm));
                }

                var totalItems = await query.CountAsync();

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(item => new PublicWorkDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        EstimatedHours = item.EstimatedHours,
                    }).ToListAsync();

                var paginatedResponse = new PaginatedResponse<PublicWorkDTO>
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

        [HttpGet("getByIdPublicWork/{id}")]
        public async Task<IActionResult> GetByIdPublicWork(int id)
        {
            var responseApi = new ResponseAPI<PublicWorkDTO>();
            try
            {
                var dbPublicWork = await _dbMechRentSaContext.PublicWorks.FirstOrDefaultAsync(x => x.Id == id);

                if (dbPublicWork != null)
                {
                    var getPublicWorkDTO = new PublicWorkDTO
                    {
                        Id = dbPublicWork.Id,
                        Name = dbPublicWork.Name,
                        EstimatedHours = dbPublicWork.EstimatedHours,
                    };

                    responseApi.IsSuccessful = true;
                    responseApi.Value = getPublicWorkDTO;
                }
                else
                {
                    responseApi.IsSuccessful = false;
                    responseApi.Message = "No se ha encontrado la obra pública que buscas por Id";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost("storePublicWork")]
        public async Task<IActionResult> StorePublicWork(PublicWorkDTO PublicWorkDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbPublicWork = new PublicWork
                {
                    Id = PublicWorkDTO.Id,
                    Name = PublicWorkDTO.Name,
                    EstimatedHours = PublicWorkDTO.EstimatedHours,
                };

                _dbMechRentSaContext.Add(dbPublicWork);
                await _dbMechRentSaContext.SaveChangesAsync();

                responseApi.IsSuccessful = true;
                responseApi.Value = dbPublicWork.Id;
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut("updatePublicWork/{id}")]
        public async Task<IActionResult> UpdatePublicWork(int id, PublicWorkDTO PublicWorkDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbPublicWork = await _dbMechRentSaContext.PublicWorks.FirstOrDefaultAsync(x => x.Id == id);

                if (dbPublicWork != null)
                {
                    dbPublicWork.Name = PublicWorkDTO.Name;
                    dbPublicWork.EstimatedHours = PublicWorkDTO.EstimatedHours;

                    _dbMechRentSaContext.Update(dbPublicWork);
                    await _dbMechRentSaContext.SaveChangesAsync();

                    responseApi.IsSuccessful = true;
                    responseApi.Value = dbPublicWork.Id;
                }
                else
                {
                    responseApi.IsSuccessful = false;
                    responseApi.Message = "No ha sido posible encontrar la obra pública con ese Id";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpDelete("deletePublicWork/{id}")]
        public async Task<IActionResult> DeletePublicWork(int id)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbPublicWork = await _dbMechRentSaContext.PublicWorks
                    .Include(e => e.ExcavatorWorkLogs)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (dbPublicWork != null)
                {
                    if (dbPublicWork.ExcavatorWorkLogs.Any())
                    {
                        responseApi.IsSuccessful = false;
                        responseApi.Message = "No se puede eliminar la obra pública porque tiene registros relacionados.";
                    }
                    else
                    {
                        _dbMechRentSaContext.PublicWorks.Remove(dbPublicWork);
                        await _dbMechRentSaContext.SaveChangesAsync();

                        responseApi.IsSuccessful = true;
                        responseApi.Value = dbPublicWork.Id;
                    }
                }
                else
                {
                    responseApi.IsSuccessful = false;
                    responseApi.Message = "No ha sido posible encontrar la obra pública con ese Id";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsSuccessful = false;
                responseApi.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
