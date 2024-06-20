using App.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Models.Configurations {
    public class BoardingPassConfiguration : IEntityTypeConfiguration<BoardingPass>

    {
        public void Configure(EntityTypeBuilder<BoardingPass> builder)
        {
            // Flight - BoardingPass : n-1
            builder.HasOne(bp => bp.Flight)
                .WithMany(f => f.BoardingPasses)
                .HasForeignKey(bp => bp.FlightId)
                .IsRequired();

            // Users - BoardingPass: n-1
            builder
                .HasOne(pdc => pdc.Passenger)
                .WithMany()
                .HasForeignKey(pdc => pdc.PassengerId)
                .IsRequired();

            //BoardingPass - TicketClass: n-n
            //BoardingPass - BoardingPass_TicketClass: n-1
            builder.HasMany(bc => bc.BoardingPass_TicketClasses)
                .WithOne(b => b.BoardingPass)
                .HasForeignKey(bc => bc.BoardingPassID)
                .IsRequired();
        }
    }
}
