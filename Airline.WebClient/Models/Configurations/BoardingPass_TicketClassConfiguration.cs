using App.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Models.Configurations
{
    public class BoardingPass_TicketClassConfiguration : IEntityTypeConfiguration<BoardingPass_TicketClass>

    {
        public void Configure(EntityTypeBuilder<BoardingPass_TicketClass> builder)
        {
            builder.HasKey(pc => new { pc.BoardingPassID, pc.TicketClassID });

            //BoardingPass - TicketClass: n-n
            //BoardingPass_TicketClass - BoardingPass: n-1
            //BoardingPass_TicketClass - TicketClass: n-1
            builder.HasOne(t => t.TicketClass)
                .WithMany(bt => bt.BoardingPass_TicketClasses)
                .HasForeignKey(bt => bt.TicketClassID)
                .IsRequired();

            builder.HasOne(bp => bp.BoardingPass)
                .WithMany(bt => bt.BoardingPass_TicketClasses)
                .HasForeignKey(bt => bt.BoardingPassID)
                .IsRequired();
        }
    }
}
