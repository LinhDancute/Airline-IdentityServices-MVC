using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.TicketId);

            // Flight - Ticket: n-1
            builder.HasOne(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FlightId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // UnitPrice - Ticket: n-1
            builder.HasOne(t => t.UnitPrice)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.PriceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Passenger - Ticket: n-1
            builder.HasOne(t => t.Passenger)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.PassengerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // TicketClass - Ticket: n-1
            builder.HasOne(t => t.TicketClass)
                .WithMany(tc => tc.Tickets)
                .HasForeignKey(t => t.ClassId)  
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket - Ticket_Meal: n-1
            builder.HasMany(t => t.Ticket_Meals)
                .WithOne(tm => tm.Ticket)
                .HasForeignKey(tm => tm.TicketID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); 

            // Ticket - Ticket_Baggage: n-1
            builder.HasMany(t => t.Ticket_Baggages)
                .WithOne(tb => tb.Ticket)
                .HasForeignKey(tb => tb.TicketID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Ticket - BoardingPass: n-1
            builder.HasMany(t => t.BoardingPasses)
                .WithOne(bp => bp.Ticket)
                .HasForeignKey(bp => bp.TicketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Ticket - InvoiceDetail: n-1
            builder.HasMany(t => t.InvoiceDetails)
                .WithOne(bp => bp.Ticket)
                .HasForeignKey(bp => bp.TicketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
