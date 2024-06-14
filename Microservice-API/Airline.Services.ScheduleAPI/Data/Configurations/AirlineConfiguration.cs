using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.Services.ScheduleAPI.Data.Configurations
{
    public class AirlineConfiguration : IEntityTypeConfiguration<Airline.Services.ScheduleAPI.Models.Airline>
    {
        public void Configure(EntityTypeBuilder<Airline.Services.ScheduleAPI.Models.Airline> builder)
        {
            builder.HasKey(a => a.AirlineId);

            builder.HasMany(f => f.Flights)
                .WithOne(a => a.Airlines)
                .HasForeignKey(f => f.AirlineId)
                .IsRequired();
        }
    }
}
