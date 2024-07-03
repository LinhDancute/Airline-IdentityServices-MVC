using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Schedule;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services.IServices.Airline;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services.Airline
{
    public class AirportService : IAirportService
    {
        private readonly IBaseService _baseService;

        public AirportService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<bool> AirportExistsAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = AirportAPIBase + $"/api/Airport/{id}"
            });

            return response != null && response.IsSuccess;
        }

        public async Task CloseAirportAsync(int id, AirportDTO airportDTO)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = AirportAPIBase + $"/api/Airport/close/{id}",
                Data = airportDTO
            });

            if (response == null || !response.IsSuccess)
            {
                throw new Exception(response?.Message ?? "Failed to close airport.");
            }
        }

        public async Task CreateAirportAsync(AirportCreateDTO airportDTO)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = AirportAPIBase + "/api/Airport",
                Data = airportDTO
            });
        }

        public async Task CreateAirportsAsync(List<AirportCreateDTO> airportDTOs)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = AirportAPIBase + "/api/Airport/bulk",
                Data = airportDTOs
            });
        }

        public async Task DeleteAirportAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                ApiUrl = $"{AirportAPIBase}/api/Airport/{id}"
            });
        }

        public async Task<AirportDTO> GetAirportByIdAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = AirportAPIBase + $"/api/Airport/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<AirportDTO>(Convert.ToString(response.Result));
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

        public async Task<AirportDTO> GetAirportByNameAsync(string airportName)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = AirportAPIBase + $"/api/Airport?name={airportName}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<AirportDTO>(Convert.ToString(response.Result));
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

        public async Task<IEnumerable<AirportDTO>> GetAllAirportsAsync()
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = AirportAPIBase + "/api/Airport"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    var responseObject = JsonConvert.DeserializeObject<ResponsesDTO>(Convert.ToString(response.Result));

                    if (responseObject.Result != null && responseObject.Result is JArray)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<AirportDTO>>(responseObject.Result.ToString());
                    }
                    else
                    {
                        // Handle case where Result is not as expected
                        throw new JsonSerializationException("Response Result is not in the expected format.");
                    }

                    return JsonConvert.DeserializeObject<IEnumerable<AirportDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    // Handle the error as needed
                    return new List<AirportDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<AirportDTO>();
            }
        }

        public async Task UpdateAirportAsync(int id, AirportCreateDTO airportDTO)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = AirportAPIBase + $"/api/Airport/{id}",
                Data = airportDTO
            });
        }
    }
}
