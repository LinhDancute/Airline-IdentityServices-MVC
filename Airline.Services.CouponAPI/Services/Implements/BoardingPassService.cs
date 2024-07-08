using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.CouponAPI.Repositories;
using AutoMapper;

namespace Airline.Services.CouponAPI.Services.Implements
{
    public class BoardingPassService : IBoardingPassService
    {
        private readonly IBoardingPassRepository _boardingPassRepository;
        private readonly IMapper _mapper;

        public BoardingPassService(IBoardingPassRepository boardingPassRepository, IMapper mapper)
        {
            _boardingPassRepository = boardingPassRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(BoardingPassDTO boardingPassDTO)
        {
            var boadingPass = _mapper.Map<BoardingPass>(boardingPassDTO);
            await _boardingPassRepository.AddAsync(boadingPass);
        }

        public async Task<IEnumerable<BoardingPassDTO>> GetAllAsync()
        {
            var boadingPasses = await _boardingPassRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BoardingPassDTO>>(boadingPasses);
        }
            
        public async Task<BoardingPassDTO> GetByIdAsync(int id)
        {
            var boadingPass = await _boardingPassRepository.GetByIdAsync(id);
            if (boadingPass != null)
            {
                return _mapper.Map<BoardingPassDTO>(boadingPass);
            }
            else
            {
                throw new KeyNotFoundException("Boading pass not found");
            }
        }
    }
}
