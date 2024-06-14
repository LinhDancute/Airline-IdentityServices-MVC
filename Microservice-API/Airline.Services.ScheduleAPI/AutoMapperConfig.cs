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
                // Mapping for Airline
                config.CreateMap<AirlineDTO, Airline.Services.ScheduleAPI.Models.Airline>();
                config.CreateMap<Airline.Services.ScheduleAPI.Models.Airline, AirlineDTO>();

                // Mapping for Airport
                config.CreateMap<AirportDTO, Airport>();
                config.CreateMap<Airport, AirportDTO>();

                // Mapping for AirportCreateDTO
                config.CreateMap<AirportCreateDTO, Airport>()
                    .ForMember(dest => dest.AirportId, opt => opt.Ignore()) // Ignore mapping AirportId
                    .ForMember(dest => dest.FlightRoute_Airports, opt => opt.Ignore()); // Ignore mapping FlightRoute_Airports

                // Reverse mappings if needed
                config.CreateMap<AirportCreateDTO, Airport>().ReverseMap();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
