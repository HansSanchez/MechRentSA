using MechRentSA.Shared;

namespace MechRentSA.Client.Interfaces
{
    public interface IPublicWorkService
    {
        /// <summary>
        /// Obtiene todas las obra públicas con paginación y búsqueda.
        /// </summary>
        /// <param name="searchTerm">El término de búsqueda.</param>
        /// <param name="pageNumber">El número de la página actual.</param>
        /// <param name="pageSize">El número de registros por página.</param>
        /// <returns>Lista de obra públicas paginadas.</returns>
        Task<PaginatedResponse<PublicWorkDTO>> GetPublicWorks(string searchTerm, int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene una obra pública por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la obra pública.</param>
        /// <returns>La obra pública correspondiente al identificador proporcionado.</returns>
        Task<PublicWorkDTO> GetByIdPublicWork(int id);

        /// <summary>
        /// Almacena una nueva obra pública.
        /// </summary>
        /// <param name="PublicWorkDTO">El DTO de la obra pública a almacenar.</param>
        /// <returns>El identificador de la obra pública almacenada.</returns>
        Task<int> StorePublicWork(PublicWorkDTO PublicWorkDTO);

        /// <summary>
        /// Actualiza una obra pública existente.
        /// </summary>
        /// <param name="PublicWorkDTO">El DTO de la obra pública a actualizar.</param>
        /// <param name="id">El identificador de la obra pública a actualizar.</param>
        /// <returns>El número de registros actualizados.</returns>
        Task<int> UpdatePublicWork(PublicWorkDTO PublicWorkDTO);

        /// <summary>
        /// Elimina una obra pública por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la obra pública a eliminar.</param>
        /// <returns>Un objeto ResponseAPI que indica si la eliminación fue exitosa.</returns>
        Task<ResponseAPI<int>> DeletePublicWork(int id);

    }
}
