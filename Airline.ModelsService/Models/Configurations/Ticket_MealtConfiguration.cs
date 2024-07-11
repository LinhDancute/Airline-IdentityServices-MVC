using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class Ticket_MealConfiguration : IEntityTypeConfiguration<Ticket_Meal>
    {
        public void Configure(EntityTypeBuilder<Ticket_Meal> builder)
        {
            builder.HasKey(tb => new { tb.TicketID, tb.MealID });

            builder.HasOne(fr => fr.Meal)
                   .WithMany(fr => fr.Ticket_Meals)
                   .HasForeignKey(fr => fr.MealID);

            builder.HasOne(fr => fr.Ticket)
                   .WithMany(a => a.Ticket_Meals)
                   .HasForeignKey(fr => fr.TicketID);
        }
    }
}
