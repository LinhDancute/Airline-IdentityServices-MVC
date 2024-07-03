using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations {
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(f => f.FlightId);

            //Flight - Airline: 1-n
            builder.HasOne(f => f.Airline)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AirlineId)
                .IsRequired();

            //Flight - FlightRoute: n-n
            //Flight - FlightRoute_Flight: n-1
            builder.HasMany(ff => ff.FlightRoute_Flights)
                .WithOne(a => a.Flight)
                .HasForeignKey(f => f.FlightID)
                .IsRequired();

            //Flight - Ticket: n-1
            builder.HasMany(t => t.Tickets)
                .WithOne(f => f.Flight)
                .HasForeignKey(f => f.FlightId)
                .IsRequired();

        }
    }
}