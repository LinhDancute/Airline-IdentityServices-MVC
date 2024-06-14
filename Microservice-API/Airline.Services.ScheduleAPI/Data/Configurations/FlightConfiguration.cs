using Airline.Services.ScheduleAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.Services.ScheduleAPI.Data.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(f => f.FlightId);

            builder.HasOne(f => f.Airlines)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AirlineId)
                .IsRequired();

            builder.HasMany(ff => ff.FlightRoute_Flights)
                .WithOne(a => a.Flights)
                .HasForeignKey(f => f.FlightID)
                .IsRequired();
        }
    }
}
