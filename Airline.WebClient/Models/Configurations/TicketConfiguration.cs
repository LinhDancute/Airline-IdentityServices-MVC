using App.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Models.Configurations {
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>

    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            // Flight - Ticket : n-1
            // UnitPrice - Ticket : n-1
            // User - Ticket : n-1

            builder.HasOne(f => f.Flight)
                .WithMany(t => t.Tickets)
                .HasForeignKey(f => f.FlightId)
                .IsRequired();

            builder.HasOne(u => u.UnitPrice)
                .WithMany(t => t.Tickets)
                .HasForeignKey(pdc => pdc.PriceId)
                .IsRequired();

            builder.HasOne(p => p.Passenger)
                .WithMany(t => t.Tickets)
                .HasForeignKey(p => p.PassengerId)
                .IsRequired();
        }
    }
}
