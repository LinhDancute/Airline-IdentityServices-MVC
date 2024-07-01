using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services.IServices.Airline;
using Newtonsoft.Json;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services.Airline
{
    public class MealService : IMealService
    {
        private readonly IBaseService _baseService;

        public MealService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<IEnumerable<MealDTO>> GetAllAsync()
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = MealAPIBase + "/api/Meal"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<MealDTO>>(Convert.ToString(response.Result));
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Error deserializing response: {ex.Message}");
                    // Handle the error as needed
                    return new List<MealDTO>();
                }
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new List<MealDTO>();
            }
        }

        public async Task<MealDTO> GetByIdAsync(int id)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = MealAPIBase + $"/api/Meal/{id}"
            });

            if (response != null && response.IsSuccess)
            {
                try
                {
                    return JsonConvert.DeserializeObject<MealDTO>(Convert.ToString(response.Result));
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

        public async Task CreateAsync(MealCreateDTO mealCreateDTO)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = MealAPIBase + "/api/Meal",
                Data = mealCreateDTO
            });
        }

        public async Task CreateBulkAsync(IEnumerable<MealCreateDTO> mealCreateDTOs)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = MealAPIBase + "/api/Meal/bulk",
                Data = mealCreateDTOs
            });
        }

        public async Task UpdateAsync(int id, MealCreateDTO mealCreateDTO)
        {
            await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = MealAPIBase + $"/api/Meal/{id}",
                Data = mealCreateDTO
            });
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _baseService.SendAsyncCouponAPI(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                ApiUrl = $"{MealAPIBase}/api/Meal/{id}"
            });
        }
    }
}
