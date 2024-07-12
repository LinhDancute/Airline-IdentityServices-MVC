using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations {
    public class BoardingPassConfiguration : IEntityTypeConfiguration<BoardingPass>

    {
        public void Configure(EntityTypeBuilder<BoardingPass> builder)
        { 
            // Ticket - BoardingPass: n-1
            builder.HasOne(bp => bp.Ticket)
                .WithMany(t => t.BoardingPasses)
                .HasForeignKey(bp => bp.TicketId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        }
    }
}
