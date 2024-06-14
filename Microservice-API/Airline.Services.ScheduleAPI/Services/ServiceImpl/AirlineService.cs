using Airline.Services.ScheduleAPI.Models.DTOs;
using Airline.Services.ScheduleAPI.Repositories;
using AutoMapper;

namespace Airline.Services.ScheduleAPI.Services.ServiceImpl
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IMapper _mapper;
        public AirlineService(IAirlineRepository airlineRepository, IMapper mapper)
        {
            _airlineRepository = airlineRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AirlineDTO>> GetAllAirlinesAsync()
        {
            var airlines = await _airlineRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AirlineDTO>>(airlines);
        }

        public async Task<AirlineDTO> GetAirlineByIdAsync(int id)
        {
            var airline = await _airlineRepository.GetByIdAsync(id);
            return _mapper.Map<AirlineDTO>(airline);
        }

        //Add single
        public async Task CreateAirlineAsync(AirlineDTO airlineDto)
        {
            // Check for duplicates
            var existingAirline = await _airlineRepository.FindAsync(a =>
                a.AirlineName == airlineDto.AirlineName ||
                a.IATAcode == airlineDto.IATACode ||
                a.ICAOcode == airlineDto.ICAOCode);

            if (existingAirline != null)
            {
                throw new InvalidOperationException("An airline with the same name, IATA code, or ICAO code already exists.");
            }

            var airline = _mapper.Map<Models.Airline>(airlineDto);
            await _airlineRepository.AddAsync(airline);
        }
        
        //Add list
        public async Task CreateAirlinesAsync(List<AirlineDTO> airlineDTOs) 
        {
            foreach (var airlineDTO in airlineDTOs)
            {
                // Check for duplicates
                var existingAirline = await _airlineRepository.FindAsync(a =>
                    a.AirlineName == airlineDTO.AirlineName ||
                    a.IATAcode == airlineDTO.IATACode ||
                    a.ICAOcode == airlineDTO.ICAOCode);

                if (existingAirline != null)
                {
                    throw new InvalidOperationException($"An airline with the name '{airlineDTO.AirlineName}', IATA code '{airlineDTO.IATACode}', or ICAO code '{airlineDTO.ICAOCode}' already exists.");
                }
            }
            var airlines = _mapper.Map<List<Models.Airline>>(airlineDTOs);
            await _airlineRepository.AddRangeAsync(airlines); 
        }

        public async Task UpdateAirlineAsync(int id, AirlineDTO airlineDto)
        {
            var existingAirline = await _airlineRepository.GetByIdAsync(id);
            if (existingAirline == null)
            {
                throw new Exception($"Airline with ID {id} not found.");
            }

            _mapper.Map(airlineDto, existingAirline);
            await _airlineRepository.UpdateAsync(existingAirline);
        }

        public async Task DeleteAirlineAsync(int id)
        {
            await _airlineRepository.DeleteAsync(id);
        }

        public async Task<bool> AirlineExistsAsync(int id)
        {
            return await _airlineRepository.AirlineExistsAsync(id);
        }

    }
}
