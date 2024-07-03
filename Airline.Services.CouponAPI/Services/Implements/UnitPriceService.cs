using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.ModelsService.Models.Statistical;
using Airline.Services.CouponAPI.Repositories;
using AutoMapper;

namespace Airline.Services.CouponAPI.Services.Implements
{
    public class UnitPriceService : IUnitPriceService
    {
        private readonly IUnitPriceRepository _repository;
        private readonly IMapper _mapper;
        private const decimal UsdToVndRate = 25000m;    // 1 USD = 25000 VND

        public UnitPriceService(IUnitPriceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnitPriceDTO>> GetAllAsync()
        {
            var unitPrices = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UnitPriceDTO>>(unitPrices);
        }

        public async Task<UnitPriceDTO> GetByIdAsync(int id)
        {
            var unitPrice = await _repository.GetByIdAsync(id);
            if (unitPrice == null)
            {
                throw new Exception($"The unit price with ID {id} does not exist.");
            }
            return _mapper.Map<UnitPriceDTO>(unitPrice);
        }

        public async Task<UnitPriceDTO> CreateAsync(UnitPriceCreateDTO unitPriceCreateDTO)
        {
            CalculateMissingPrice(unitPriceCreateDTO);
            var unitPrice = _mapper.Map<UnitPrice>(unitPriceCreateDTO);
            await _repository.AddAsync(unitPrice);
            return _mapper.Map<UnitPriceDTO>(unitPrice);
        }


        public async Task CreateBulkAsync(IEnumerable<UnitPriceCreateDTO> unitPriceCreateDTOs)
        {
            foreach (var dto in unitPriceCreateDTOs)
            {
                CalculateMissingPrice(dto);
            }
            var unitPrices = _mapper.Map<IEnumerable<UnitPrice>>(unitPriceCreateDTOs);
            await _repository.AddRangeAsync(unitPrices);
        }

        public async Task UpdateAsync(int id, UnitPriceCreateDTO unitPriceCreateDTO)
        {
            var unitPrice = await _repository.GetByIdAsync(id);
            if (unitPrice == null)
            {
                throw new Exception($"The unit price with ID {id} does not exist.");
            }

            CalculateMissingPrice(unitPriceCreateDTO);
            _mapper.Map(unitPriceCreateDTO, unitPrice);
            await _repository.UpdateAsync(unitPrice);
        }

        public async Task DeleteAsync(int id)
        {
            var ticketClass = await _repository.GetByIdAsync(id);
            if (ticketClass == null)
            {
                throw new Exception($"The unit price with ID {id} does not exist.");
            }

            await _repository.DeleteAsync(ticketClass);
        }

        private void CalculateMissingPrice(UnitPriceCreateDTO unitPriceCreateDTO)
        {
            if (unitPriceCreateDTO.USD == 0 && unitPriceCreateDTO.VND > 0)
            {
                unitPriceCreateDTO.USD = unitPriceCreateDTO.VND / UsdToVndRate;
            }
            else if (unitPriceCreateDTO.VND == 0 && unitPriceCreateDTO.USD > 0)
            {
                unitPriceCreateDTO.VND = unitPriceCreateDTO.USD * UsdToVndRate;
            }
        }
    }
}
