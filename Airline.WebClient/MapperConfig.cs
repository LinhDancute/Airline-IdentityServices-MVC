using Airline.WebClient.Models.Airline;
using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Models.DTOs.Schedule;
using AutoMapper;

namespace Airline.WebClient
{
    public static class MapperConfig
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                // Mapping
                // Airline
                config.CreateMap<AirlineDTO, Models.Airline.Airline>();
                config.CreateMap<Models.Airline.Airline, AirlineDTO>();

                //Airport
                config.CreateMap<AirportDTO, Airport>();
                config.CreateMap<Airport, AirportDTO>();

                //AirportCreateDTO
                config.CreateMap<AirportCreateDTO, Airport>()
                    .ForMember(dest => dest.AirportId, opt => opt.Ignore()) // Ignore mapping AirportId
                    .ForMember(dest => dest.FlightRoute_Airports, opt => opt.Ignore()); // Ignore mapping FlightRoute_Airports

                //FlightRoute
                config.CreateMap<FlightRouteDTO, FlightRoute>();
                config.CreateMap<FlightRoute, FlightRouteDTO>();

                //FlightRoute_Airport
                config.CreateMap<FlightRoute_AirportDTO, FlightRoute_Airport>();
                config.CreateMap<FlightRoute_Airport,  FlightRoute_AirportDTO>();

                //Flight
                config.CreateMap<FlightCreateDTO, Flight>();
                config.CreateMap<Flight, FlightCreateDTO>();
                config.CreateMap<Flight, FlightDTO>()
                    .ForMember(dest => dest.FlightRouteIds, opt => opt.MapFrom(src => src.FlightRoute_Flights.Select(fr => fr.FlightRouteID).ToList()));

                //TicketClass
                config.CreateMap<TicketClass, TicketClassDTO>();
                config.CreateMap<TicketClassCreateDTO, TicketClass>();

                // Reverse mappings
                config.CreateMap<Models.DTOs.Schedule.AirlineDTO, Models.Airline.Airline>().ReverseMap();
                config.CreateMap<AirportCreateDTO, Airport>().ReverseMap();
                config.CreateMap<FlightRouteCreateDTO, FlightRoute>().ReverseMap();
                config.CreateMap<FlightRoute_AirportDTO, FlightRoute_Airport>().ReverseMap();
                config.CreateMap<FlightDTO, Flight>().ReverseMap();
                config.CreateMap<TicketClassDTO, TicketClass>().ReverseMap();


            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
