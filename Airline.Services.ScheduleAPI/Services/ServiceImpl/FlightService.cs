using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.ScheduleAPI.Repositories;
using Airline.Services.ScheduleAPI.Repositories.RepositoryImpl;
using AutoMapper;

namespace Airline.Services.ScheduleAPI.Services.ServiceImpl
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAirlineRepository _airlineRepository;
        private readonly IFlightRouteRepository _flightRouteRepository;
        private readonly IFlightRoute_FlightRepository _flightRoute_FlightRepository;
        private readonly IMapper _mapper;

        public FlightService(
            IFlightRepository flightRepository,
            IAirlineRepository airlineRepository,
            IFlightRouteRepository flightRouteRepository,
            IFlightRoute_FlightRepository flightRoute_AirportRepository,
            IMapper mapper)
        {
            _flightRepository = flightRepository;
            _airlineRepository = airlineRepository;
            _flightRouteRepository = flightRouteRepository; 
            _flightRoute_FlightRepository = flightRoute_AirportRepository;
            _mapper = mapper;
        }

        //Get all
        public async Task<IEnumerable<FlightDTO>> GetAllFlightsAsync()
        {
            var flights = await _flightRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FlightDTO>>(flights);
        }

        // Create a single flight
        public async Task CreateFlightAsync(FlightCreateDTO flightDTO)
        {
            // Validate and map DTO to entity
            var flight = _mapper.Map<FlightCreateDTO, Flight>(flightDTO);

            // Validate flight sector existence
            var flightSector = await _flightRouteRepository.FindAsync(a => a.FlightSector == flightDTO.FlightSector);
            if (flightSector == null)
            {
                throw new Exception($"The flight route with sector {flightDTO.FlightSector} does not exist.");
            }

            // Validate and get FlightRoutes by sector from DTO
            var flightRoutes = await _flightRouteRepository.GetBySectorAsync(flightDTO.FlightSector);
            if (flightRoutes == null || !flightRoutes.Any())
            {
                throw new Exception("One or more flight routes provided do not exist.");
            }

            flight.FlightSector = string.Join(", ", flightRoutes.Select(fr => fr.FlightSector));

            // Validate and get Airline by IATA code from Aircraft in DTO
            var airline = await _airlineRepository.FindByIATACodeAsync(flightDTO.Aircraft);
            if (airline == null)
            {
                throw new Exception($"The airline with IATA code {flightDTO.Aircraft} does not exist.");
            }
            else
            {
                flight.Aircraft = airline.IATAcode;
                flight.AirlineId = airline.AirlineId;
                flight.Airline = airline;
            }

            // Set other properties
            flight.Date = flightDTO.Date.Date;
            flight.FlightNumber = flightDTO.FlightNumber;
            flight.DepartureTime = flightDTO.DepartureTime;
            flight.ArrivalTime = flightDTO.ArrivalTime;
            flight.FlightTime = flightDTO.FlightTime;
            flight.EconomySeat = flightDTO.EconomySeat;
            flight.PremiumEconomySeat = flightDTO.PremiumEconomySeat;
            flight.BusinessSeat = flightDTO.BusinessSeat;
            flight.Status = (Flight.FlightStatusType)flightDTO.Status;

            // Add flight to repository
            await _flightRepository.AddAsync(flight);

            // Associate flight with flight routes
            foreach (var fr in flightRoutes)
            {
                // Create new FlightRoute_Flight entry
                var flightRouteFlight = new FlightRoute_Flight
                {
                    FlightID = flight.FlightId,
                    FlightRouteID = fr.FlightRouteId
                };

                // Add to repository
                await _flightRoute_FlightRepository.AddAsync(flightRouteFlight);
            }
        }


        //create list
        public async Task CreateFlightsAsync(List<FlightCreateDTO> flightDTOs)
        {
            foreach (var flightDTO in flightDTOs)
            {
                // Validate and map DTO to entity
                var flight = _mapper.Map<FlightCreateDTO, Flight>(flightDTO);

                // Validate flight sector existence
                var flightSector = await _flightRouteRepository.FindAsync(a => a.FlightSector == flightDTO.FlightSector);
                if (flightSector == null)
                {
                    throw new Exception($"The flight route with sector {flightDTO.FlightSector} does not exist.");
                }

                // Validate and get FlightRoutes by sector from DTO
                var flightRoutes = await _flightRouteRepository.GetBySectorAsync(flightDTO.FlightSector);
                if (flightRoutes == null || !flightRoutes.Any())
                {
                    throw new Exception("One or more flight routes provided do not exist.");
                }

                flight.FlightSector = string.Join(", ", flightRoutes.Select(fr => fr.FlightSector));

                // Validate and get Airline by IATA code from Aircraft in DTO
                var airline = await _airlineRepository.FindByIATACodeAsync(flightDTO.Aircraft);
                if (airline == null)
                {
                    throw new Exception($"The airline with IATA code {flightDTO.Aircraft} does not exist.");
                }
                else
                {
                    flight.Aircraft = airline.IATAcode;
                    flight.AirlineId = airline.AirlineId;
                    flight.Airline = airline;
                }

                // Set other properties
                flight.Date = flightDTO.Date.Date;
                flight.FlightNumber = flightDTO.FlightNumber;
                flight.DepartureTime = flightDTO.DepartureTime;
                flight.ArrivalTime = flightDTO.ArrivalTime;
                flight.FlightTime = flightDTO.FlightTime;
                flight.EconomySeat = flightDTO.EconomySeat;
                flight.PremiumEconomySeat = flightDTO.PremiumEconomySeat;
                flight.BusinessSeat = flightDTO.BusinessSeat;
                flight.Status = (Flight.FlightStatusType)flightDTO.Status;

                // Add flight to repository
                await _flightRepository.AddAsync(flight);

                // Associate flight with flight routes
                foreach (var fr in flightRoutes)
                {
                    var flightRouteFlight = new FlightRoute_Flight
                    {
                        FlightID = flight.FlightId,
                        FlightRouteID = fr.FlightRouteId
                    };

                    // Add to repository
                    await _flightRoute_FlightRepository.AddAsync(flightRouteFlight);
                }
            }
        }
        public async Task UpdateFlightAsync(int flightId, FlightCreateDTO flightDTO)
        {
            // Retrieve existing flight
            var flight = await _flightRepository.FindAsync(f => f.FlightId == flightId);
            if (flight == null)
            {
                throw new Exception($"The flight with ID {flightId} does not exist.");
            }

            // Validate flight sector existence
            var flightSector = await _flightRouteRepository.FindAsync(a => a.FlightSector == flightDTO.FlightSector);
            if (flightSector == null)
            {
                throw new Exception($"The flight route with sector {flightDTO.FlightSector} does not exist.");
            }

            // Validate and get FlightRoutes by sector from DTO
            var flightRoutes = await _flightRouteRepository.GetBySectorAsync(flightDTO.FlightSector);
            if (flightRoutes == null || !flightRoutes.Any())
            {
                throw new Exception("One or more flight routes provided do not exist.");
            }

            flight.FlightSector = string.Join(", ", flightRoutes.Select(fr => fr.FlightSector));

            // Validate and get Airline by IATA code from Aircraft in DTO
            var airline = await _airlineRepository.FindByIATACodeAsync(flightDTO.Aircraft);
            if (airline == null)
            {
                throw new Exception($"The airline with IATA code {flightDTO.Aircraft} does not exist.");
            }
            else
            {
                flight.Aircraft = airline.IATAcode;
                flight.AirlineId = airline.AirlineId;
                flight.Airline = airline;
            }

            // Update flight properties
            flight.Date = flightDTO.Date.Date;
            flight.FlightNumber = flightDTO.FlightNumber;
            flight.DepartureTime = flightDTO.DepartureTime;
            flight.ArrivalTime = flightDTO.ArrivalTime;
            flight.FlightTime = flightDTO.FlightTime;
            flight.EconomySeat = flightDTO.EconomySeat;
            flight.PremiumEconomySeat = flightDTO.PremiumEconomySeat;
            flight.BusinessSeat = flightDTO.BusinessSeat;
            flight.Status = (Flight.FlightStatusType)flightDTO.Status;

            // Update flight in repository
            await _flightRepository.UpdateAsync(flight);

            // Associate flight with flight routes
            var existingFlightRoutes = await _flightRoute_FlightRepository.FindAllAsync(frf => frf.FlightID == flight.FlightId);
            var existingFlightRouteIds = new HashSet<int>(existingFlightRoutes.Select(frf => frf.FlightRouteID));

            foreach (var fr in flightRoutes)
            {
                if (!existingFlightRouteIds.Contains(fr.FlightRouteId))
                {
                    var flightRouteFlight = new FlightRoute_Flight
                    {
                        FlightID = flight.FlightId,
                        FlightRouteID = fr.FlightRouteId
                    };

                    await _flightRoute_FlightRepository.AddAsync(flightRouteFlight);
                }
            }
        }

        public async Task DeleteFlightAsync(int flightId)
        {
            // Retrieve existing flight
            var flight = await _flightRepository.FindAsync(f => f.FlightId == flightId);
            if (flight == null)
            {
                throw new Exception($"The flight with ID {flightId} does not exist.");
            }

            // Remove associations with flight routes
            var flightRouteFlights = await _flightRoute_FlightRepository.FindAllAsync(frf => frf.FlightID == flight.FlightId);
            foreach (var flightRouteFlight in flightRouteFlights)
            {
                await _flightRoute_FlightRepository.DeleteAsync(flightRouteFlight.FlightRouteID); 
            }

            // Delete flight from repository
            await _flightRepository.DeleteAsync(flight.FlightId);
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _flightRepository.FindAsync(fr => fr.FlightId == id);
        }


        public async Task CloseFlightAsync(int flightId)
        {
            // Retrieve existing flight
            var flight = await _flightRepository.FindAsync(f => f.FlightId == flightId);
            if (flight == null)
            {
                throw new Exception($"The flight with ID {flightId} does not exist.");
            }

            // Set status to inactive
            flight.Status = Flight.FlightStatusType.Inactive;

            // Update flight in repository
            await _flightRepository.UpdateAsync(flight);
        }

        public async Task<IEnumerable<FlightDTO>> SearchFlightsByRouteAsync(FlightSearchDTO flightSearchDTO)
        {
            // Check if the flight sector exists
            var flightRoute = await _flightRouteRepository.FindAsync(fr =>
                fr.DepartureAddress == flightSearchDTO.DepartureAddress &&
                fr.ArrivalAddress == flightSearchDTO.ArrivalAddress);

            if (flightRoute == null)
            {
                throw new InvalidOperationException($"No flight route found for sector {flightSearchDTO.DepartureAddress}-{flightSearchDTO.ArrivalAddress}.");
            }

            // Search for flights with the matching flight sector and date range
            var flights = await _flightRepository.SearchFlightsAsync(flightSearchDTO.FromDate, flightSearchDTO.ToDate, flightRoute.FlightSector);
            return _mapper.Map<IEnumerable<FlightDTO>>(flights);
        }

    }
}
