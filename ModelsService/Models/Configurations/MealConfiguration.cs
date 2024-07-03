using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>

    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasKey(tc => tc.MealId);

            //TicketClass - Ticket: n-1
            builder.HasMany(bpt => bpt.Tickets)
                .WithOne(tc => tc.Meal)
                .HasForeignKey(bpt => bpt.MealId)
                .IsRequired();
        }
    }
}
