using MechRentSA.Client.Interfaces;
using MechRentSA.Shared;
using System.Net.Http.Json;

namespace MechRentSA.Client.Services
{
    public class ExcavatorService: IExcavatorService
    {

        private readonly HttpClient _http;

        public ExcavatorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<PaginatedResponse<ExcavatorDTO>> GetExcavators(string searchTerm, int pageNumber, int pageSize)
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<PaginatedResponse<ExcavatorDTO>>>($"api/Excavator/getExcavator?searchTerm={searchTerm}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }


        public async Task<ExcavatorDTO> GetByIdExcavator(int id)
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<ExcavatorDTO>>($"api/Excavator/getByIdExcavator/{id}");
            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<int> StoreExcavator(ExcavatorDTO excavatorDTO)
        {
            var request = await _http.PostAsJsonAsync("api/Excavator/storeExcavator", excavatorDTO);
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

        public async Task<int> UpdateExcavator(ExcavatorDTO excavatorDTO)
        {
            var request = await _http.PutAsJsonAsync($"api/Excavator/updateExcavator/{excavatorDTO.Id}", excavatorDTO);
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

        public async Task<ResponseAPI<int>> DeleteExcavator(int id)
        {
            var request = await _http.DeleteAsync($"api/Excavator/deleteExcavator/{id}");
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

        public async Task<List<ExcavatorDTO>> GetExcavatorsNearMaintenance()
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<List<ExcavatorDTO>>>("api/Excavator/getExcavatorsNearMaintenance");
            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }
    }
}
