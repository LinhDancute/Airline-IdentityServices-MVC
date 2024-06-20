using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Models.Configurations
{
    public class AirlineConfiguration : IEntityTypeConfiguration<App.Models.Airline.Airline>
    {
        public void Configure(EntityTypeBuilder<App.Models.Airline.Airline> builder)
        {
            builder.HasKey(a => a.AirlineId);

            builder.HasMany(f => f.Flights)
                .WithOne(a => a.Airline)
                .HasForeignKey(f => f.AirlineId)
                .IsRequired();
        }
    }
}
