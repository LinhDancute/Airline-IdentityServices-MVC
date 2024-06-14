using Airline.Services.ScheduleAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.Services.ScheduleAPI.Data.Configurations
{
    public class FlightRouteConfiguration : IEntityTypeConfiguration<FlightRoute>
    {
        public void Configure(EntityTypeBuilder<FlightRoute> builder)
        {
            builder.HasKey(fr => fr.FlightRouteId);

            builder.HasMany(fra => fra.FlightRoute_Airports)
                .WithOne(fr => fr.FlightRoutes)
                .HasForeignKey(fra => fra.FlightRouteID)
                .IsRequired();

            builder.HasMany(frf => frf.FlightRoute_Flights)
                .WithOne(fr => fr.FlightRoutes)
                .HasForeignKey(frf => frf.FlightRouteID)
                .IsRequired();
        }
    }
}
