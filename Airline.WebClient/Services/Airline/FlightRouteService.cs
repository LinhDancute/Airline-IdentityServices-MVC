using Airline.WebClient.Models.Airline;
using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Schedule;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services.IServices.Airline;
using Newtonsoft.Json;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services.Airline
{
    public class FlightRouteService : IFlightRouteService
    {
        private readonly IBaseService _baseService;

        public FlightRouteService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task CloseFlightRouteAsync(int id, FlightRouteCreateDTO flightRouteDTO)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = $"{FlightRouteAPIBase}/api/FlightRoute/close/{id}",
                Data = flightRouteDTO
            });
        }

        public async Task CreateFlightRouteAsync(FlightRouteCreateDTO flightRouteDTO)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = FlightRouteAPIBase + "/api/FlightRoute",
                Data = flightRouteDTO
            });
        }

        public async Task CreateFlightRoutesAsync(List<FlightRouteCreateDTO> flightRouteDTOs)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = FlightRouteAPIBase + "/api/FlightRoute/bulk",
                Data = flightRouteDTOs
            });
        }

        public async Task DeleteFlightRouteAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                ApiUrl = $"{FlightRouteAPIBase}/api/FlightRoute/{id}"
            });
        }

        public async Task<bool> FlightRouteExistsAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = $"{FlightRouteAPIBase}/api/FlightRoute/exists/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<bool>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return false;
            }
        }

        public async Task<Airport> GetAirportByAbbreviationAsync(string abbreviation)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = $"{FlightRouteAPIBase}/api/FlightRoute/airport/{abbreviation}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<Airport>(Convert.ToString(response.Result));
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

        public async Task<IEnumerable<FlightRouteDTO>> GetAllFlightRoutesAsync()
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = FlightRouteAPIBase + "/api/FlightRoute"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<FlightRouteDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    // Handle the error as needed
                    return new List<FlightRouteDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<FlightRouteDTO>();
            }
        }

        public async Task<FlightRoute> GetFlightRouteByIdAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = FlightRouteAPIBase + $"/api/FlightRoute/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<FlightRoute>(Convert.ToString(response.Result));
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

        public async Task UpdateFlightRouteAsync(int id, FlightRouteCreateDTO flightRouteDTO)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = FlightRouteAPIBase + $"/api/FlightRoute/{id}",
                Data = flightRouteDTO
            });
        }
    }
}
