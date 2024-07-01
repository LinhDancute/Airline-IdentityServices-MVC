using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class TicketClassConfiguration : IEntityTypeConfiguration<TicketClass>

    {
        public void Configure(EntityTypeBuilder<TicketClass> builder)
        {
            builder.HasKey(tc => tc.TicketId);

            //TicketClass - TicketClass_Baggage: n-1
            //TicketClass - Ticket: n-1

            builder.HasMany(bpt => bpt.TicketClass_Baggages)
                .WithOne(tc => tc.TicketClass)
                .HasForeignKey(bpt => bpt.TicketClassID)
                .IsRequired();

            builder.HasMany(bpt => bpt.Tickets)
                .WithOne(tc => tc.TicketClass)
                .HasForeignKey(bpt => bpt.TicketId)
                .IsRequired();
        }
    }
}
