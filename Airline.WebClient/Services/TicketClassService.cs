using Newtonsoft.Json;
using static Airline.WebClient.Utility.SD;
using Airline.WebClient.Utility;
using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Services.IServices;

namespace Airline.WebClient.Services
{
    public class TicketClassService : ITicketClassService
    {
        private readonly IBaseService _baseService;

        public TicketClassService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<List<TicketClassDTO>> GetAllTicketClassesAsync()
        {
            var request = new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = SD.TicketClassAPIBase + "/api/ticketclass"
            };

            var response = await _baseService.SendAsync(request);
            if (response != null && response.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<TicketClassDTO>>(Convert.ToString(response.Result));
            }

            return new List<TicketClassDTO>();
        }
    }
}
