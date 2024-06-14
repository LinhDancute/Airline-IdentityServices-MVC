using Airline.Services.ScheduleAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.Services.ScheduleAPI.Data.Configurations
{
    public class AirportConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.HasKey(a => a.AirportId);

            builder.HasMany(fa => fa.FlightRoute_Airports)
                .WithOne(a => a.Airports)
                .HasForeignKey(fa => fa.AirportID)
                .IsRequired();
        }
    }
}
