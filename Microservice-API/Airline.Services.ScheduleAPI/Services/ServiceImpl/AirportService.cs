using Airline.Services.ScheduleAPI.Models;
using Airline.Services.ScheduleAPI.Models.DTOs;
using Airline.Services.ScheduleAPI.Repositories;
using AutoMapper;

namespace Airline.Services.ScheduleAPI.Services.ServiceImpl
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;
        public AirportService(IAirportRepository airportRepository, IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AirportDTO>> GetAllAirportsAsync()
        {
            var airports = await _airportRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AirportDTO>>(airports);
        }

        public async Task<AirportDTO> GetAirportByIdAsync(int id)
        {
            var airport = await _airportRepository.GetByIdAsync(id);
            return _mapper.Map<AirportDTO>(airport);
        }

        public async Task<AirportDTO> GetAirportByNameAsync(string airportName)
        {
            var airport = await _airportRepository.FindAsync(a => a.AirportName == airportName);
            return _mapper.Map<AirportDTO>(airport);
        }

        //Add single
        public async Task CreateAirportAsync(AirportCreateDTO airportDTO)
        {
            // Check for duplicates
            var existingAirport = await _airportRepository.FindAsync(a =>
                a.AirportName == airportDTO.AirportName ||
                a.Abbreviation == airportDTO.Abbreviation);

            if (existingAirport != null)
            {
                throw new InvalidOperationException("An airport with the same AirportName, Abbreviation already exists.");
            }

            var airport = _mapper.Map<Airport>(airportDTO);
            airport.Status = Airport.AirportStatus.Active;
            await _airportRepository.AddAsync(airport);
        }

        //Add list
        public async Task CreateAirportsAsync(List<AirportCreateDTO> airportDTOs)
        {
            var airportsToAdd = new List<Airport>();

            foreach (var airportDTO in airportDTOs)
            {
                var existingAirport = await _airportRepository.FindAsync(a =>
                    a.AirportName == airportDTO.AirportName ||
                    a.Abbreviation == airportDTO.Abbreviation);

                if (existingAirport != null)
                {
                    throw new InvalidOperationException($"An airport with the name '{airportDTO.AirportName}' or Abbreviation '{airportDTO.Abbreviation}' already exists.");
                }

                var airport = _mapper.Map<Airport>(airportDTO);
                airport.Status = Airport.AirportStatus.Active;  
                airportsToAdd.Add(airport);
            }

            await _airportRepository.AddRangeAsync(airportsToAdd);
        }

        public async Task UpdateAirportAsync(int id, AirportCreateDTO airportDTO)
        {
            var existingAirport = await _airportRepository.GetByIdAsync(id);
            if (existingAirport == null)
            {
                throw new Exception($"Airport with ID {id} not found.");
            }

            _mapper.Map(airportDTO, existingAirport);
            await _airportRepository.UpdateAsync(existingAirport);
        }

        public async Task CloseAirportAsync(int id, AirportDTO airportDTO)
        {
            var existingAirport = await _airportRepository.GetByIdAsync(id);
            if (existingAirport == null)
            {
                throw new Exception($"Airport with ID {id} not found.");
            }

            _mapper.Map(airportDTO, existingAirport);
            existingAirport.Status = Airport.AirportStatus.Closed;
            await _airportRepository.UpdateAsync(existingAirport);
        }

        public async Task DeleteAirportAsync(int id)
        {
            await _airportRepository.DeleteAsync(id);
        }

        public async Task<bool> AirportExistsAsync(int id)
        {
            return await _airportRepository.AirportExistsAsync(id);
        }
    }
}
