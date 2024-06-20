using Airline.Services.ScheduleAPI.Repositories;
using Airline.Services.ScheduleAPI.Repositories.RepositoryImpl;
using Airline.WebClient.Models.DTOs.Schedule;
using App.Models.Airline;
using AutoMapper;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Services.ServiceImpl
{
    public class FlightRoute_AirportService : IFlightRoute_AirportService
    {
        private readonly IFlightRoute_AirportRepository _flightRoute_AirportRepository;
        private readonly IFlightRouteRepository _flightRouteRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public FlightRoute_AirportService(
            IFlightRoute_AirportRepository flightRoute_AirportRepository, 
            IFlightRouteRepository flightRouteRepository, 
            IAirportRepository airportRepository, 
            IMapper mapper)
        {
            _flightRoute_AirportRepository = flightRoute_AirportRepository;
            _flightRouteRepository = flightRouteRepository;
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        //Get all
        public async Task<IEnumerable<FlightRoute_AirportDTO>> GetAllFlightRoutes_AirportsAsync()
        {
            var flightRoutes = await _flightRoute_AirportRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FlightRoute_AirportDTO>>(flightRoutes);
        }

        //add single
        public async Task CreateFlightRoutes_AirportsAsync(FlightRoute_AirportDTO flightRoute_AirportDTO)
        {
            var existingflightRoute_Airport = await _flightRoute_AirportRepository.FindAsync(a =>
                a.FlightRouteID == flightRoute_AirportDTO.FlightRouteID &&
                a.AirportID == flightRoute_AirportDTO.AirportID);

            if (existingflightRoute_Airport != null)
            {
                throw new InvalidOperationException($"already exists.");
            }

            var flightRoute_Airport = _mapper.Map<FlightRoute_Airport>(flightRoute_AirportDTO);
           
            await _flightRoute_AirportRepository.AddAsync(flightRoute_Airport);
        }

        //add list
        public async Task CreateFlightRoutes_AirportsAsync(List<FlightRoute_AirportDTO> flightRoute_AirportDTOs)
        {
            var flightRoute_AirportsToAdd = new List<FlightRoute_Airport>();

            foreach (var flightRoute_AirportDTO in flightRoute_AirportDTOs)
            {
                var existingflightRoute_Airport = await _flightRoute_AirportRepository.FindAsync(a =>
                    a.FlightRouteID == flightRoute_AirportDTO.FlightRouteID &&
                    a.AirportID == flightRoute_AirportDTO.AirportID);

                if (existingflightRoute_Airport != null)
                {
                    throw new InvalidOperationException($"already exists.");
                }

                var flightRoute_Airport = _mapper.Map<FlightRoute_Airport>(flightRoute_AirportDTO);
                flightRoute_AirportsToAdd.Add(flightRoute_Airport);
            }

            await _flightRoute_AirportRepository.AddRangeAsync(flightRoute_AirportsToAdd);
        }

    }
}
