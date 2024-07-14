using MechRentSA.Shared;

namespace MechRentSA.Client.Interfaces
{
    public interface IExcavatorWorkLogService
    {
        /// <summary>
        /// Obtiene los registros de trabajo de retroexcavadoras con paginación.
        /// </summary>
        /// <param name="pageNumber">El número de la página actual.</param>
        /// <param name="pageSize">El número de registros por página.</param>
        /// <returns>Lista de registros de trabajo de retroexcavadoras paginadas.</returns>
        Task<List<ExcavatorWorkLogDTO>> GetExcavatorWorkLogs(int pageNumber, int pageSize);
    }

}
