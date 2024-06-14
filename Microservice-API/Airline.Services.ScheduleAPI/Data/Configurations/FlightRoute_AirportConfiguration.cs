using Airline.Services.ScheduleAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.Services.ScheduleAPI.Data.Configurations
{
    public class FlightRoute_AirportConfiguration : IEntityTypeConfiguration<FlightRoute_Airport>
    {
        public void Configure(EntityTypeBuilder<FlightRoute_Airport> builder)
        {
            builder.HasKey(fr => new { fr.FlightRouteID, fr.AirportID });

            builder.HasOne(fr => fr.FlightRoutes)
                   .WithMany(fr => fr.FlightRoute_Airports)
                   .HasForeignKey(fr => fr.FlightRouteID);

            builder.HasOne(fr => fr.Airports)
                   .WithMany(a => a.FlightRoute_Airports)
                   .HasForeignKey(fr => fr.AirportID);
        }
    }
}
