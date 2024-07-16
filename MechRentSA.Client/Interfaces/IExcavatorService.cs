using MechRentSA.Shared;

namespace MechRentSA.Client.Interfaces
{
    public interface IExcavatorService
    {
        /// <summary>
        /// Obtiene todas las retroexcavadoras con paginación y búsqueda.
        /// </summary>
        /// <param name="searchTerm">El término de búsqueda.</param>
        /// <param name="pageNumber">El número de la página actual.</param>
        /// <param name="pageSize">El número de registros por página.</param>
        /// <returns>Lista de retroexcavadoras paginadas.</returns>
        Task<PaginatedResponse<ExcavatorDTO>> GetExcavators(string searchTerm, int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene una retroexcavadora por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la retroexcavadora.</param>
        /// <returns>La retroexcavadora correspondiente al identificador proporcionado.</returns>
        Task<ExcavatorDTO> GetByIdExcavator(int id);

        /// <summary>
        /// Almacena una nueva retroexcavadora.
        /// </summary>
        /// <param name="excavatorDTO">El DTO de la retroexcavadora a almacenar.</param>
        /// <returns>El identificador de la retroexcavadora almacenada.</returns>
        Task<int> StoreExcavator(ExcavatorDTO excavatorDTO);

        /// <summary>
        /// Actualiza una retroexcavadora existente.
        /// </summary>
        /// <param name="excavatorDTO">El DTO de la retroexcavadora a actualizar.</param>
        /// <param name="id">El identificador de la retroexcavadora a actualizar.</param>
        /// <returns>El número de registros actualizados.</returns>
        Task<int> UpdateExcavator(ExcavatorDTO excavatorDTO);

        /// <summary>
        /// Elimina una retroexcavadora por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la retroexcavadora a eliminar.</param>
        /// <returns>Un objeto ResponseAPI que indica si la eliminación fue exitosa.</returns>
        Task<ResponseAPI<int>> DeleteExcavator(int id);

        Task<List<ExcavatorDTO>> GetExcavatorsNearMaintenance();

    }
}
