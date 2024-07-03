using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Schedule;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services.IServices.Airline;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services.Airline
{
    public class AirlineService : IAirlineService
    {
        private readonly IBaseService _baseService;

        public AirlineService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<bool> AirlineExistsAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = $"{AirlineAPIBase}/api/Airline/{id}"
            });

            return response != null && response.IsSuccess;
        }

        public async Task CreateAirlineAsync(AirlineDTO airlineDto)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = AirlineAPIBase + "/api/Airline",
                Data = airlineDto
            });
        }

        public async Task CreateAirlinesAsync(List<AirlineDTO> airlineDTOs)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = AirlineAPIBase + "/api/Airline/bulk",
                Data = airlineDTOs
            });
        }

        public async Task DeleteAirlineAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                ApiUrl = $"{AirlineAPIBase}/api/Airline/{id}"

            });
        }

        public async Task<AirlineDTO> GetAirlineByIdAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = AirlineAPIBase + $"/api/Airline/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<AirlineDTO>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<AirlineDTO>> GetAllAirlinesAsync()
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = AirlineAPIBase + "/api/Airline"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    var responseObject = JsonConvert.DeserializeObject<ResponsesDTO>(Convert.ToString(response.Result));

                    if (responseObject.Result != null && responseObject.Result is JArray)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<AirlineDTO>>(responseObject.Result.ToString());
                    }
                    else
                    {
                        // Handle case where Result is not as expected
                        throw new JsonSerializationException("Response Result is not in the expected format.");
                    }

                    return JsonConvert.DeserializeObject<IEnumerable<AirlineDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    // Handle the error as needed
                    return new List<AirlineDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<AirlineDTO>();
            }
        }


        public async Task UpdateAirlineAsync(int id, AirlineDTO airlineDto)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = AirlineAPIBase + $"/api/Airline/{id}",
                Data = airlineDto
            });
        }
    }
}
