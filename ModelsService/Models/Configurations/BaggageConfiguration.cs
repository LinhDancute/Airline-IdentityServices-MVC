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

            //Ticket - Ticket_Baggage - Baggage: n - 1 - n
            builder.HasMany(bpt => bpt.Ticket_Baggages)
                .WithOne(tc => tc.Baggage)
                .HasForeignKey(bpt => bpt.BaggageID)
                .IsRequired();
        }
    }
}
