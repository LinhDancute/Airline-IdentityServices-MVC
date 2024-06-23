using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class FlightRouteConfiguration : IEntityTypeConfiguration<FlightRoute>
    {
        public void Configure(EntityTypeBuilder<FlightRoute> builder)
        {
            builder.HasKey(fr => fr.FlightRouteId);

            builder.HasMany(fra => fra.FlightRoute_Airports)
                .WithOne(fr => fr.FlightRoute)
                .HasForeignKey(fra => fra.FlightRouteID)
                .IsRequired();

            builder.HasMany(frf => frf.FlightRoute_Flights)
                .WithOne(fr => fr.FlightRoute)
                .HasForeignKey(frf => frf.FlightRouteID)
                .IsRequired();
        }
    }
}
