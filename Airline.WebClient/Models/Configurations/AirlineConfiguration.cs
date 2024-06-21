using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Models.Configurations
{
    public class AirlineConfiguration : IEntityTypeConfiguration<App.Models.Airline.Airline>
    {
        public void Configure(EntityTypeBuilder<App.Models.Airline.Airline> builder)
        {
            builder.HasKey(a => a.AirlineId);

            builder.HasOne(a => a.ParentAirline)
                .WithMany(a => a.AirlineChildren)
                .HasForeignKey(a => a.ParentAirlineId);

            builder.HasMany(f => f.Flights)
                .WithOne(a => a.Airline)
                .HasForeignKey(f => f.AirlineId)
                .IsRequired();
        }
    }
}
