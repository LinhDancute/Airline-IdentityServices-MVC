using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services.IServices.Airline;
using Newtonsoft.Json;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services.Airline
{
    public class UnitPriceService : IUnitPriceService
    {
        private readonly IBaseService _baseService;

        public UnitPriceService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<IEnumerable<UnitPriceDTO>> GetAllAsync()
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = UnitPriceAPIBase + "/api/UnitPrice"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<UnitPriceDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    return new List<UnitPriceDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<UnitPriceDTO>();
            }
        }

        public async Task<UnitPriceDTO> GetByIdAsync(int id)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = UnitPriceAPIBase + $"/api/UnitPrice/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<UnitPriceDTO>(Convert.ToString(response.Result));
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

        public async Task<UnitPriceDTO> CreateAsync(UnitPriceCreateDTO unitPriceCreateDTO)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = UnitPriceAPIBase + "/api/UnitPrice",
                Data = unitPriceCreateDTO
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<UnitPriceDTO>(Convert.ToString(response.Result));
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

        public async Task CreateBulkAsync(IEnumerable<UnitPriceCreateDTO> unitPriceCreateDTOs)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = UnitPriceAPIBase + "/api/UnitPrice/bulk",
                Data = unitPriceCreateDTOs
            });
        }

        public async Task UpdateAsync(int id, UnitPriceCreateDTO unitPriceCreateDTO)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = UnitPriceAPIBase + $"/api/UnitPrice/{id}",
                Data = unitPriceCreateDTO
            });
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                ApiUrl = $"{UnitPriceAPIBase}/api/UnitPrice/{id}"
            });
        }
    }
}
