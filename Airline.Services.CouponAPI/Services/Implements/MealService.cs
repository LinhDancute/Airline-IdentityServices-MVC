using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.ModelsService.Models.Airline;
using Airline.Services.CouponAPI.Repositories;
using AutoMapper;

namespace Airline.Services.CouponAPI.Services.Implements
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _mapper;

        public MealService(IMealRepository mealRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(MealCreateDTO mealDTO)
        {
            var meal = _mapper.Map<Meal>(mealDTO);
            await _mealRepository.AddAsync(meal);
        }

        public async Task CreateBulkAsync(IEnumerable<MealCreateDTO> mealDTOs)
        {
            var meals = _mapper.Map<IEnumerable<Meal>>(mealDTOs);
            await _mealRepository.AddRangeAsync(meals);
        }

        public async Task DeleteAsync(int id)
        {
            var meal = await _mealRepository.GetByIdAsync(id);
            if (meal != null)
            {
                await _mealRepository.DeleteAsync(meal);
            }
            else
            {
                throw new KeyNotFoundException("Meal not found");
            }
        }

        public async Task<IEnumerable<MealDTO>> GetAllAsync()
        {
            var meals = await _mealRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MealDTO>>(meals);
        }

        public async Task<MealDTO> GetByIdAsync(int id)
        {
            var meal = await _mealRepository.GetByIdAsync(id);
            if (meal != null)
            {
                return _mapper.Map<MealDTO>(meal);
            }
            else
            {
                throw new KeyNotFoundException("Meal not found");
            }
        }

        public async Task UpdateAsync(int id, MealCreateDTO mealDTO)
        {
            var existingMeal = await _mealRepository.GetByIdAsync(id);
            if (existingMeal != null)
            {
                _mapper.Map(mealDTO, existingMeal);
                await _mealRepository.UpdateAsync(existingMeal);
            }
            else
            {
                throw new KeyNotFoundException("Meal not found");
            }
        }
    }
}
