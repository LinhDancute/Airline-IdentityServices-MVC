using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class Ticket_BaggageConfiguration : IEntityTypeConfiguration<Ticket_Baggage>
    {
        public void Configure(EntityTypeBuilder<Ticket_Baggage> builder)
        {
            builder.HasKey(tb => new { tb.TicketID, tb.BaggageID });

            builder.HasOne(fr => fr.Baggage)
                   .WithMany(fr => fr.Ticket_Baggages)
                   .HasForeignKey(fr => fr.BaggageID);

            builder.HasOne(fr => fr.Ticket)
                   .WithMany(a => a.Ticket_Baggages)
                   .HasForeignKey(fr => fr.TicketID);
        }
    }
}
