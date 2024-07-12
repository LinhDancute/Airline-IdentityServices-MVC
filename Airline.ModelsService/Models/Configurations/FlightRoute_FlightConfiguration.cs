using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class FlightRoute_FlightConfiguration : IEntityTypeConfiguration<FlightRoute_Flight>
    {
        public void Configure(EntityTypeBuilder<FlightRoute_Flight> builder)
        {
            builder.HasKey(ff => new { ff.FlightRouteID, ff.FlightID });

            builder.HasOne(ff => ff.FlightRoute)
                   .WithMany(fr => fr.FlightRoute_Flights)
                   .HasForeignKey(ff => ff.FlightRouteID);

            builder.HasOne(ff => ff.Flight)
                   .WithMany(f => f.FlightRoute_Flights)
                   .HasForeignKey(ff => ff.FlightID);
        }
    }
}
