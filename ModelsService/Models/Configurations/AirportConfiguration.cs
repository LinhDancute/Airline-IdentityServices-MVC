using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class AirportConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.HasKey(a => a.AirportId);

            builder.HasMany(fa => fa.FlightRoute_Airports)
                .WithOne(a => a.Airport)
                .HasForeignKey(fa => fa.AirportID)
                .IsRequired();
        }
    }
}
