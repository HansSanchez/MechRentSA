using MechRentSA.Shared;

namespace MechRentSA.Client.Interfaces
{
    public interface IPublicWorkService
    {
        /// <summary>
        /// Obtiene todas las obras públicas con paginación.
        /// </summary>
        /// <param name="pageNumber">El número de la página actual.</param>
        /// <param name="pageSize">El número de registros por página.</param>
        /// <returns>Lista de obras públicas paginadas.</returns>
        Task<List<PublicWorkDTO>> GetPublicWorks(int pageNumber, int pageSize);
    }
}
