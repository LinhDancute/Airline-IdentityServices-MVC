using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations {
    public class BoardingPassConfiguration : IEntityTypeConfiguration<BoardingPass>

    {
        public void Configure(EntityTypeBuilder<BoardingPass> builder)
        {
            // Flight - BoardingPass : n-1
            builder.HasOne(bp => bp.Flight)
                .WithMany(f => f.BoardingPasses)
                .HasForeignKey(bp => bp.FlightId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Users - BoardingPass: n-1
            builder.HasOne(bp => bp.Passenger)
                .WithMany(u => u.BoardingPasses)
                .HasForeignKey(bp => bp.PassengerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Meal - BoardingPass : n-1
            builder.HasOne(bp => bp.Meal)
                .WithMany(m => m.BoardingPasses)
                .HasForeignKey(bp => bp.MealId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Ticket - BoardingPass: n-1
            builder.HasOne(bp => bp.Ticket)
                .WithMany(t => t.BoardingPasses)
                .HasForeignKey(bp => bp.TicketId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // TicketClass - BoardingPass : n-1
            builder.HasOne(bp => bp.TicketClass)
                .WithMany(tc => tc.BoardingPasses)
                .HasForeignKey(bp => bp.ClassId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Baggage - BoardingPass: n-1
            builder.HasOne(bp => bp.Baggage)
                .WithMany(b => b.BoardingPasses)
                .HasForeignKey(bp => bp.BaggageId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
