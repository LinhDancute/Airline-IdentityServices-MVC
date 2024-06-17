using Airline.Services.ScheduleAPI.Models;
using Airline.Services.ScheduleAPI.Models.DTOs;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Airline.Services.ScheduleAPI
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                // Mapping
                // Airline
                config.CreateMap<AirlineDTO, Airline.Services.ScheduleAPI.Models.Airline>();
                config.CreateMap<Airline.Services.ScheduleAPI.Models.Airline, AirlineDTO>();

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

                //FlightRouteCreateDTO
                config.CreateMap<FlightRouteCreateDTO, FlightRoute>()
                    .ForMember(dest => dest.FlightRouteId, opt => opt.Ignore()) 
                    .ForMember(dest => dest.FlightRoute_Flights, opt => opt.Ignore()); // Ignore mapping FlightRoute_Flights

                //FlightRoute_Airport
                config.CreateMap<FlightRoute_AirportDTO, FlightRoute_Airport>();
                config.CreateMap<FlightRoute_Airport,  FlightRoute_AirportDTO>();

                // Reverse mappings
                config.CreateMap<AirportCreateDTO, Airport>().ReverseMap();
                config.CreateMap<FlightRouteCreateDTO, FlightRoute>().ReverseMap();
                config.CreateMap<FlightRoute_AirportDTO, FlightRoute_Airport>().ReverseMap();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
