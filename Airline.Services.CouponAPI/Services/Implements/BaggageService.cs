using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Repositories;
using AutoMapper;

namespace Airline.Services.CouponAPI.Services.Implements
{
    public class BaggageService : IBaggageService
    {
        private readonly IBaggageRepository _baggageRepository;
        private readonly IMapper _mapper;

        public BaggageService(IBaggageRepository baggageRepository, IMapper mapper)
        {
            _baggageRepository = baggageRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(BaggageCreateDTO baggageCreateDTO)
        {
            var baggage = _mapper.Map<Baggage>(baggageCreateDTO);
            await _baggageRepository.AddAsync(baggage);
        }

        public async Task CreateBulkAsync(IEnumerable<BaggageCreateDTO> baggageCreateDTOs)
        {
            var baggages = _mapper.Map<IEnumerable<Baggage>>(baggageCreateDTOs);
            await _baggageRepository.AddRangeAsync(baggages);
        }

        public async Task DeleteAsync(int id)
        {
            var baggage = await _baggageRepository.GetByIdAsync(id);
            if (baggage != null)
            {
                await _baggageRepository.DeleteAsync(baggage);
            }
            else
            {
                throw new KeyNotFoundException("baggage not found");
            }
        }

        public async Task<IEnumerable<BaggageDTO>> GetAllAsync()
        {
            var baggages = await _baggageRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BaggageDTO>>(baggages);
        }

        public async Task<BaggageDTO> GetByIdAsync(int id)
        {
            var baggage = await _baggageRepository.GetByIdAsync(id);
            if (baggage != null)
            {
                return _mapper.Map<BaggageDTO>(baggage);
            }
            else
            {
                throw new KeyNotFoundException("baggage not found");
            }
        }

        public async Task UpdateAsync(int id, BaggageCreateDTO baggageCreateDTO)
        {
            var existingBaggage = await _baggageRepository.GetByIdAsync(id);
            if (existingBaggage != null)
            {
                _mapper.Map(baggageCreateDTO, existingBaggage);
                await _baggageRepository.UpdateAsync(existingBaggage);
            }
            else
            {
                throw new KeyNotFoundException("Baggage not found");
            }
        }
    }
}
