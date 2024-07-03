using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services.IServices.Airline;
using Newtonsoft.Json;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services.Airline
{
    public class BaggageService : IBaggageService
    {
        private readonly IBaseService _baseService;

        public BaggageService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<IEnumerable<BaggageDTO>> GetAllAsync()
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = BaggageAPIBase + "/api/Baggage"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<BaggageDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    // Handle the error as needed
                    return new List<BaggageDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<BaggageDTO>();
            }
        }

        public async Task<BaggageDTO> GetByIdAsync(int id)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = BaggageAPIBase + $"/api/Baggage/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<BaggageDTO>(Convert.ToString(response.Result));
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

        public async Task CreateAsync(BaggageCreateDTO baggageCreateDTO)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = BaggageAPIBase + "/api/Baggage",
                Data = baggageCreateDTO
            });
        }

        public async Task CreateBulkAsync(IEnumerable<BaggageCreateDTO> baggageCreateDTOs)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = BaggageAPIBase + "/api/Baggage/bulk",
                Data = baggageCreateDTOs
            });
        }

        public async Task UpdateAsync(int id, BaggageCreateDTO baggageCreateDTO)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = BaggageAPIBase + $"/api/Baggage/{id}",
                Data = baggageCreateDTO
            });
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                ApiUrl = $"{BaggageAPIBase}/api/Baggage/{id}"
            });
        }
    }
}
