using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            // Flight - Ticket : n-1
            builder.HasOne(f => f.Flight)
                .WithMany(t => t.Tickets)
                .HasForeignKey(f => f.FlightId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // UnitPrice - Ticket : n-1
            builder.HasOne(u => u.UnitPrice)
                .WithMany(t => t.Tickets)
                .HasForeignKey(pdc => pdc.PriceId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // User - Ticket : n-1
            builder.HasOne(p => p.Passenger)
                .WithMany(t => t.Tickets)
                .HasForeignKey(p => p.PassengerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // TicketClass - Ticket : n-1
            builder.HasOne(f => f.TicketClass)
                .WithMany(t => t.Tickets)
                .HasForeignKey(f => f.ClassId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Meal - Ticket : n-1
            builder.HasOne(u => u.Meal)
                .WithMany(t => t.Tickets)
                .HasForeignKey(pdc => pdc.MealId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Baggage - Ticket : n-1
            builder.HasOne(p => p.Baggage)
                .WithMany(t => t.Tickets)
                .HasForeignKey(p => p.BaggageId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // BoardingPass - Ticket: 1-n
            builder.HasMany(bp => bp.BoardingPasses)
                .WithOne(t => t.Ticket)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }

    }
}
