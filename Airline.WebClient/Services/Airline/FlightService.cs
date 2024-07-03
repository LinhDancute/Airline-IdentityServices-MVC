using Airline.WebClient.Models.Airline;
using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Schedule;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services.IServices.Airline;
using Newtonsoft.Json;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services.Airline
{
    public class FlightService : IFlightService
    {
        private readonly IBaseService _baseService;

        public FlightService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task CloseFlightAsync(int flightId)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.PATCH,
                ApiUrl = $"{FlightAPIBase}/api/Flight/{flightId}/close"
            });

            if (!response.IsSuccess)
            {
                throw new Exception($"Failed to close flight with ID {flightId}. Error: {response.Message}");
            }
        }

        public async Task CreateFlightAsync(FlightCreateDTO flightDTO)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = FlightAPIBase + "/api/Flight",
                Data = flightDTO
            });
        }

        public async Task CreateFlightsAsync(List<FlightCreateDTO> flightDTOs)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = FlightAPIBase + "/api/Flight/bulk",
                Data = flightDTOs
            });
        }

        public async Task DeleteFlightAsync(int flightId)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                ApiUrl = $"{FlightAPIBase}/api/Flight/{flightId}"
            });
        }

        public async Task<IEnumerable<FlightDTO>> GetAllFlightsAsync()
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = FlightAPIBase + "/api/Flight"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<FlightDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    // Handle the error as needed
                    return new List<FlightDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<FlightDTO>();
            }
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = FlightAPIBase + $"/api/Flight/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<Flight>(Convert.ToString(response.Result));
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
        
        public async Task UpdateFlightAsync(int flightId, FlightCreateDTO flightDTO)
        {
            await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = FlightAPIBase + $"/api/Flight/{flightId}",
                Data = flightDTO
            });
        }
    }
}
