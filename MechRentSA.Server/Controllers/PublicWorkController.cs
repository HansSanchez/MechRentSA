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

        [HttpGet]
        [Route("getPublicWork")]
        public async Task<IActionResult> GetPublicWork(int pageNumber = 1, int pageSize = 10)
        {
            var responseApi = new ResponseAPI<List<PublicWorkDTO>>();
            var getPublicWorkDTO = new List<PublicWorkDTO>();

            try
            {
                // Validación de parámetros de paginación
                if (pageNumber <= 0)
                    pageNumber = 1;
                if (pageSize <= 0)
                    pageSize = 10;

                // Obtener los registros con paginación
                var query = _dbMechRentSaContext.PublicWorks
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(item => new PublicWorkDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        EstimatedHours = item.EstimatedHours
                    });

                getPublicWorkDTO = await query.ToListAsync();

                responseApi.IsSuccessful = true;
                responseApi.Value = getPublicWorkDTO;
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
