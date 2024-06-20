using App.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.WebClient.Models.Configurations
{
    public class TicketClassConfiguration : IEntityTypeConfiguration<TicketClass>

    {
        public void Configure(EntityTypeBuilder<TicketClass> builder)
        {
            builder.HasKey(tc => tc.TicketId);

            builder.HasMany(bpt => bpt.BoardingPass_TicketClasses)
                .WithOne(tc => tc.TicketClass)
                .HasForeignKey(bpt => bpt.TicketClassID)
                .IsRequired();
        }
    }
}
