using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Schedule;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services.IServices.Airline;
using Newtonsoft.Json;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services.Airline
{
    public class FlightRoute_AirportService : IFlightRoute_AirportService
    {
        private readonly IBaseService _baseService;

        public FlightRoute_AirportService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task CreateFlightRoutes_AirportsAsync(FlightRoute_AirportDTO flightRoute_AirportDTO)
        {
            throw new NotImplementedException();
        }

        public async Task CreateFlightRoutes_AirportsAsync(List<FlightRoute_AirportDTO> flightRoute_AirportDTOs)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FlightRoute_AirportDTO>> GetAllFlightRoutes_AirportsAsync()
        {
            var response = await _baseService.SendAsyncScheduleAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = FlightRoute_AirportAPIBase + "/api/FlightRoute_Airport"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<FlightRoute_AirportDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    // Handle the error as needed
                    return new List<FlightRoute_AirportDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<FlightRoute_AirportDTO>();
            }
        }
    }
}
