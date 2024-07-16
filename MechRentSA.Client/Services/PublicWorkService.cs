using MechRentSA.Client.Interfaces;
using MechRentSA.Shared;
using System.Net.Http.Json;

namespace MechRentSA.Client.Services
{
    public class PublicWorkService : IPublicWorkService
    {

        private readonly HttpClient _http;

        public PublicWorkService(HttpClient http)
        {
            _http = http;
        }

        public async Task<PaginatedResponse<PublicWorkDTO>> GetPublicWorks(string searchTerm, int pageNumber, int pageSize)
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<PaginatedResponse<PublicWorkDTO>>>($"api/PublicWork/getPublicWork?searchTerm={searchTerm}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }


        public async Task<PublicWorkDTO> GetByIdPublicWork(int id)
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<PublicWorkDTO>>($"api/PublicWork/getByIdPublicWork/{id}");
            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<int> StorePublicWork(PublicWorkDTO PublicWorkDTO)
        {
            var request = await _http.PostAsJsonAsync("api/PublicWork/storePublicWork", PublicWorkDTO);
            var response = await request.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<int> UpdatePublicWork(PublicWorkDTO PublicWorkDTO)
        {
            var request = await _http.PutAsJsonAsync($"api/PublicWork/updatePublicWork/{PublicWorkDTO.Id}", PublicWorkDTO);
            var response = await request.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<ResponseAPI<int>> DeletePublicWork(int id)
        {
            var request = await _http.DeleteAsync($"api/PublicWork/deletePublicWork/{id}");
            if (!request.IsSuccessStatusCode)
            {
                return new ResponseAPI<int> { IsSuccessful = false, Message = "Error al eliminar la excavadora en el servidor." };
            }

            var response = await request.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response == null)
            {
                return new ResponseAPI<int> { IsSuccessful = false, Message = "Respuesta del servidor vacía o inválida." };
            }

            return response;
        }


    }
}
