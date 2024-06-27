using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Services.IServices;
using Newtonsoft.Json;
using static Airline.WebClient.Utilities.SD;
using Airline.WebClient.Services.IServices.Airline;

namespace Airline.WebClient.Services.Airline
{
    public class TicketClassService : ITicketClassService
    {
        private readonly IBaseService _baseService;

        public TicketClassService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<IEnumerable<TicketClassDTO>> GetAllAsync()
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = TicketClassAPIBase + "/api/TicketClass"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TicketClassDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    // Handle the error as needed
                    return new List<TicketClassDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<TicketClassDTO>();
            }
        }

        public async Task<TicketClassDTO> GetByIdAsync(int id)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = TicketClassAPIBase + $"/api/TicketClass/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<TicketClassDTO>(Convert.ToString(response.Result));
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

        public async Task CreateAsync(TicketClassCreateDTO ticketClassDTO)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = TicketClassAPIBase + "/api/TicketClass",
                Data = ticketClassDTO
            });
        }

        public async Task CreateBulkAsync(IEnumerable<TicketClassCreateDTO> ticketClassDTOs)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = TicketClassAPIBase + "/api/TicketClass/bulk",
                Data = ticketClassDTOs
            });
        }

        public async Task UpdateAsync(int id, TicketClassCreateDTO ticketClassDTO)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = TicketClassAPIBase + $"/api/TicketClass/{id}",
                Data = ticketClassDTO
            });
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                ApiUrl = $"{TicketClassAPIBase}/api/TicketClass/{id}"
            });
        }
    }
}
