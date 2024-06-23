using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.ScheduleAPI.Repositories;
using Airline.Services.ScheduleAPI.Repositories.RepositoryImpl;
using AutoMapper;

namespace Airline.Services.ScheduleAPI.Services.ServiceImpl
{
    public class FlightRouteService : IFlightRouteService
    {
        private readonly IFlightRouteRepository _flightRouteRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IFlightRoute_AirportRepository _flightRoute_AirportRepository;
        private readonly IMapper _mapper;

        public FlightRouteService(
            IFlightRouteRepository flightRouteRepository,
            IAirportRepository airportRepository,
            IFlightRoute_AirportRepository flightRoute_AirportRepository,
            IMapper mapper)
        {
            _flightRoute_AirportRepository = flightRoute_AirportRepository;
            _flightRouteRepository = flightRouteRepository;
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        //Get all
        public async Task<IEnumerable<FlightRouteDTO>> GetAllFlightRoutesAsync()
        {
            var flightRoutes = await _flightRouteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FlightRouteDTO>>(flightRoutes);
        }

        //create single
        public async Task CreateFlightRouteAsync(FlightRouteCreateDTO flightRouteDTO)
        {
            var departureAirport = await _airportRepository.FindAsync(a => a.Abbreviation == flightRouteDTO.DepartureAddress);
            var arrivalAirport = await _airportRepository.FindAsync(a => a.Abbreviation == flightRouteDTO.ArrivalAddress);

            // Check if departure or arrival airports are unavailable
            if (departureAirport == null || departureAirport.Status == Airport.AirportStatus.Closed)
            {
                throw new InvalidOperationException("Departure airport is closed or does not exist.");
            }

            if (arrivalAirport == null || arrivalAirport.Status == Airport.AirportStatus.Closed)
            {
                throw new InvalidOperationException("Arrival airport is closed or does not exist.");
            }

            // Check if the departure is the same as the arrival
            if (flightRouteDTO.DepartureAddress == flightRouteDTO.ArrivalAddress)
            {
                throw new InvalidOperationException("Departure address cannot be the same as arrival address.");
            }

            // Check if a flight route with the same departure and arrival addresses already exists
            var existingRoute = await _flightRouteRepository.FindAsync(fr =>
                fr.DepartureAddress == flightRouteDTO.DepartureAddress &&
                fr.ArrivalAddress == flightRouteDTO.ArrivalAddress);

            if (existingRoute != null)
            {
                throw new InvalidOperationException("A flight route with the same departure and arrival addresses already exists.");
            }

            // Map DTO to entity and create a new flight route
            var flightRoute = new FlightRoute
            {
                DepartureAddress = flightRouteDTO.DepartureAddress,
                ArrivalAddress = flightRouteDTO.ArrivalAddress,
                FlightSector = $"{flightRouteDTO.DepartureAddress}-{flightRouteDTO.ArrivalAddress}",
                FlightSectorName = $"{departureAirport.AirportName} - {arrivalAirport.AirportName}",
                Gate = (FlightRoute.GateStatusType)flightRouteDTO.Gate,
                Status = (FlightRoute.FlightRouteStatusType)flightRouteDTO.Status,
            };

            await _flightRouteRepository.AddAsync(flightRoute);

            // Create FlightRoute_Airport entries
            var flightRouteAirportEntries = new List<FlightRoute_Airport>
            {
                new FlightRoute_Airport { FlightRouteID = flightRoute.FlightRouteId, AirportID = departureAirport.AirportId },
                new FlightRoute_Airport { FlightRouteID = flightRoute.FlightRouteId, AirportID = arrivalAirport.AirportId }
            };

            foreach (var entry in flightRouteAirportEntries)
            {
                await _flightRoute_AirportRepository.AddAsync(entry);
            }
            
        }


        //create list
        public async Task CreateFlightRoutesAsync(List<FlightRouteCreateDTO> flightRouteDTOs)
        {
            foreach (var flightRouteDTO in flightRouteDTOs)
            {
                var departureAirport = await _airportRepository.FindAsync(a => a.Abbreviation == flightRouteDTO.DepartureAddress);
                var arrivalAirport = await _airportRepository.FindAsync(a => a.Abbreviation == flightRouteDTO.ArrivalAddress);

                // Check if departure or arrival airports are unavailable
                if (departureAirport == null || departureAirport.Status == Airport.AirportStatus.Closed)
                {
                    throw new InvalidOperationException("Departure airport is closed or does not exist.");
                }

                if (arrivalAirport == null || arrivalAirport.Status == Airport.AirportStatus.Closed)
                {
                    throw new InvalidOperationException("Arrival airport is closed or does not exist.");
                }

                // Check if a flight route with the same departure and arrival addresses already exists
                var existingRoute = await _flightRouteRepository.FindAsync(fr =>
                    fr.DepartureAddress == flightRouteDTO.DepartureAddress &&
                    fr.ArrivalAddress == flightRouteDTO.ArrivalAddress);

                if (existingRoute != null)
                {
                    throw new InvalidOperationException("A flight route with the same departure and arrival addresses already exists.");
                }

                // Check if departure address is the same as arrival address
                if (flightRouteDTO.DepartureAddress == flightRouteDTO.ArrivalAddress)
                {
                    throw new InvalidOperationException("Departure address cannot be the same as arrival address.");
                }

                // Map DTO to entity and create a new flight route
                var flightRoute = new FlightRoute
                {
                    DepartureAddress = flightRouteDTO.DepartureAddress,
                    ArrivalAddress = flightRouteDTO.ArrivalAddress,
                    FlightSector = $"{flightRouteDTO.DepartureAddress}-{flightRouteDTO.ArrivalAddress}",
                    FlightSectorName = $"{departureAirport.AirportName} - {arrivalAirport.AirportName}",
                    Gate = (FlightRoute.GateStatusType)flightRouteDTO.Gate,
                    Status = (FlightRoute.FlightRouteStatusType)flightRouteDTO.Status,
                };

                await _flightRouteRepository.AddAsync(flightRoute);

                // Create FlightRoute_Airport entries
                var flightRouteAirportEntries = new List<FlightRoute_Airport>
                {
                    new FlightRoute_Airport { FlightRouteID = flightRoute.FlightRouteId, AirportID = departureAirport.AirportId },
                    new FlightRoute_Airport { FlightRouteID = flightRoute.FlightRouteId, AirportID = arrivalAirport.AirportId }
                };

                foreach (var entry in flightRouteAirportEntries)
                {
                    await _flightRoute_AirportRepository.AddAsync(entry);
                }
            }
        }

        //get abbreviation
        public async Task<Airport> GetAirportByAbbreviationAsync(string abbreviation)
        {
            return await _airportRepository.FindAsync(a => a.Abbreviation == abbreviation);
        }

        //get by id
        public async Task<FlightRoute> GetFlightRouteByIdAsync(int id)
        {
            return await _flightRouteRepository.FindAsync(fr => fr.FlightRouteId == id);
        }

        //update
        public async Task UpdateFlightRouteAsync(int id, FlightRouteCreateDTO flightRouteDTO)
        {
            var flightRouteToUpdate = await _flightRouteRepository.GetByIdAsync(id);

            if (flightRouteToUpdate == null)
            {
                throw new InvalidOperationException("Flight route not found.");
            }

            var departureAirport = await _airportRepository.FindAsync(a => a.Abbreviation == flightRouteDTO.DepartureAddress);
            var arrivalAirport = await _airportRepository.FindAsync(a => a.Abbreviation == flightRouteDTO.ArrivalAddress);

            // Check if departure or arrival airports are unavailable
            if (departureAirport == null || departureAirport.Status == Airport.AirportStatus.Closed)
            {
                throw new InvalidOperationException("Departure airport is closed or does not exist.");
            }

            if (arrivalAirport == null || arrivalAirport.Status == Airport.AirportStatus.Closed)
            {
                throw new InvalidOperationException("Arrival airport is closed or does not exist.");
            }

            // Check if the departure is the same as the arrival
            if (flightRouteDTO.DepartureAddress == flightRouteDTO.ArrivalAddress)
            {
                throw new InvalidOperationException("Departure address cannot be the same as arrival address.");
            }

            //update properties from DTO
            flightRouteToUpdate.DepartureAddress = flightRouteDTO.DepartureAddress;
            flightRouteToUpdate.ArrivalAddress = flightRouteDTO.ArrivalAddress;
            flightRouteToUpdate.FlightSector = $"{flightRouteDTO.DepartureAddress}-{flightRouteDTO.ArrivalAddress}";
            flightRouteToUpdate.FlightSectorName = $"{departureAirport.AirportName} - {arrivalAirport.AirportName}";
            flightRouteToUpdate.Gate = (FlightRoute.GateStatusType)flightRouteDTO.Gate;
            flightRouteToUpdate.Status = (FlightRoute.FlightRouteStatusType)flightRouteDTO.Status;

            await _flightRouteRepository.UpdateAsync(flightRouteToUpdate);

            //update FlightRoute_Airport entries
            var existingEntries = await _flightRoute_AirportRepository.FindAllAsync(fra => fra.FlightRouteID == flightRouteToUpdate.FlightRouteId);
            var newEntries = new List<FlightRoute_Airport>
            {
                new FlightRoute_Airport { FlightRouteID = flightRouteToUpdate.FlightRouteId, AirportID = departureAirport.AirportId },
                new FlightRoute_Airport { FlightRouteID = flightRouteToUpdate.FlightRouteId, AirportID = arrivalAirport.AirportId }
            };

            foreach (var entry in newEntries)
            {
                var existingEntry = existingEntries.FirstOrDefault(e => e.AirportID == entry.AirportID);
                if (existingEntry == null)
                {
                    await _flightRoute_AirportRepository.AddAsync(entry);
                }
            }

            //remove any old entries 
            foreach (var existingEntry in existingEntries)
            {
                if (!newEntries.Any(ne => ne.AirportID == existingEntry.AirportID))
                {
                    await _flightRoute_AirportRepository.DeleteAsync(existingEntry);
                }
            }
        }


        //close flight route
        public async Task CloseFlightRouteAsync(int id, FlightRouteCreateDTO flightRouteDTO)
        {
            var flightRouteToClose = await _flightRouteRepository.GetByIdAsync(id);

            if (flightRouteToClose == null)
            {
                throw new InvalidOperationException("Flight route not found.");
            }

            flightRouteToClose.Status = FlightRoute.FlightRouteStatusType.Inactive;

            await _flightRouteRepository.UpdateAsync(flightRouteToClose);
        }

        //delete
        public async Task DeleteFlightRouteAsync(int id)
        {
            var flightRouteToDelete = await _flightRouteRepository.GetByIdAsync(id);

            if (flightRouteToDelete == null)
            {
                throw new InvalidOperationException("Flight route not found.");
            }

            await _flightRouteRepository.DeleteAsync(id);
        }

        //existed?
        public async Task<bool> FlightRouteExistsAsync(int id)
        {
            return await _flightRouteRepository.FlightRouteExistsAsync(id);
        }
    }
}
