using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Airline.ModelsService.Models.Airline;

namespace Airline.ModelsService.Models.Configurations
{
    public class BaggageConfiguration : IEntityTypeConfiguration<Baggage>

    {
        public void Configure(EntityTypeBuilder<Baggage> builder)
        {
            builder.HasKey(tc => tc.BaggageId);

            //Baggage - Ticket: n-1
            //Baggage - TicketClass_Baggage: n-1

            builder.HasMany(bpt => bpt.Tickets)
                .WithOne(tc => tc.Baggage)
                .HasForeignKey(bpt => bpt.BaggageId)
                .IsRequired();

            builder.HasMany(bpt => bpt.TicketClass_Baggages)
                .WithOne(tc => tc.Baggage)
                .HasForeignKey(bpt => bpt.BaggageID)
                .IsRequired();

        }
    }
}
