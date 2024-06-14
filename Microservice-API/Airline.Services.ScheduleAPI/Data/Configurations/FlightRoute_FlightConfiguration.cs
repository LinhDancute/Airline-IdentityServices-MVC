using Airline.Services.ScheduleAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.Services.ScheduleAPI.Data.Configurations
{
    public class FlightRoute_FlightConfiguration : IEntityTypeConfiguration<FlightRoute_Flight>
    {
        public void Configure(EntityTypeBuilder<FlightRoute_Flight> builder)
        {
            builder.HasKey(ff => new { ff.FlightRouteID, ff.FlightID });

            builder.HasOne(ff => ff.FlightRoutes)
                   .WithMany(fr => fr.FlightRoute_Flights)
                   .HasForeignKey(ff => ff.FlightRouteID);

            builder.HasOne(ff => ff.Flights)
                   .WithMany(f => f.FlightRoute_Flights)
                   .HasForeignKey(ff => ff.FlightID);
        }
    }
}
