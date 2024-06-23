using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class TicketClass_BaggageConfiguration : IEntityTypeConfiguration<TicketClass_Baggage>

    {
        public void Configure(EntityTypeBuilder<TicketClass_Baggage> builder)
        {
            builder.HasKey(tb => new { tb.TicketClassID, tb.BaggageID });

            //Baggage - TicketClass: n-n
            //TicketClass_Baggage - Baggage: 1-n
            //TicketClass_Baggage - TicketClass: 1-n
            builder.HasOne(t => t.TicketClass)
                .WithMany(bt => bt.TicketClass_Baggages)
                .HasForeignKey(bt => bt.TicketClassID)
                .IsRequired();

            builder.HasOne(bp => bp.Baggage)
                .WithMany(bt => bt.TicketClass_Baggages)
                .HasForeignKey(bt => bt.BaggageID)
                .IsRequired();
        }
    }
}
