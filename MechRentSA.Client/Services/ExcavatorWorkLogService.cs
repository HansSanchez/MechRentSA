using MechRentSA.Client.Interfaces;
using MechRentSA.Shared;
using System.Net.Http.Json;

namespace MechRentSA.Client.Services
{
    public class ExcavatorWorkLogService : IExcavatorWorkLogService
    {
        private readonly HttpClient _http;

        public ExcavatorWorkLogService(HttpClient http)
        {
            _http = http;
        }

        public async Task<PaginatedResponse<ExcavatorWorkLogDTO>> GetExcavatorWorkLogs(string searchTerm, int pageNumber, int pageSize)
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<PaginatedResponse<ExcavatorWorkLogDTO>>>($"api/ExcavatorWorkLog/getExcavatorWorkLog?searchTerm={searchTerm}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<int> AddWorkHours(AddWorkHoursDTO dto)
        {
            var request = await _http.PostAsJsonAsync("api/ExcavatorWorkLog/addWorkHours", dto);
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

        public async Task<decimal> GetExcavatorWorkValue(int publicWorkId, int excavatorId)
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<decimal>>($"api/ExcavatorWorkLog/getExcavatorWorkValue/{publicWorkId}/{excavatorId}");
            if (response!.IsSuccessful)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<List<ExcavatorDTO>> GetExcavatorsNearMaintenance()
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<List<ExcavatorDTO>>>("api/ExcavatorWorkLog/getExcavatorsNearMaintenance");
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
