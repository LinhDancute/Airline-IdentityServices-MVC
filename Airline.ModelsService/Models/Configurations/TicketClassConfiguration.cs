using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class TicketClassConfiguration : IEntityTypeConfiguration<TicketClass>

    {
        public void Configure(EntityTypeBuilder<TicketClass> builder)
        {
            builder.HasKey(tc => tc.TicketClassId);

            //TicketClass - Ticket: n-1

            builder.HasMany(bpt => bpt.Tickets)
                .WithOne(tc => tc.TicketClass)
                .HasForeignKey(bpt => bpt.ClassId)
                .IsRequired();
        }
    }
}
