using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.ModelsService.Models.Statistical;
using AutoMapper;

namespace Airline.ModelsService
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                // Mapping
                // Airline
                config.CreateMap<AirlineDTO, Airline.ModelsService.Models.Airline.Airline>();
                config.CreateMap<Airline.ModelsService.Models.Airline.Airline, AirlineDTO>();

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

                //Meal
                config.CreateMap<Meal, MealDTO>();
                config.CreateMap<MealCreateDTO, Meal>();

                //Baggage
                config.CreateMap<Baggage, BaggageDTO>();
                config.CreateMap<BaggageCreateDTO, Baggage>();

                //UnitPrice
                config.CreateMap<UnitPrice, UnitPriceDTO>();
                config.CreateMap<UnitPriceCreateDTO, UnitPrice>();

                // Reverse mappings
                config.CreateMap<AirportCreateDTO, Airport>().ReverseMap();
                config.CreateMap<FlightRouteCreateDTO, FlightRoute>().ReverseMap();
                config.CreateMap<FlightRoute_AirportDTO, FlightRoute_Airport>().ReverseMap();
                config.CreateMap<FlightDTO, Flight>().ReverseMap();
                config.CreateMap<TicketClassDTO, TicketClass>().ReverseMap();
                config.CreateMap<MealDTO, Meal>().ReverseMap();
                config.CreateMap<BaggageDTO,  Baggage>().ReverseMap();
                config.CreateMap<UnitPriceDTO, UnitPrice>().ReverseMap();


            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
