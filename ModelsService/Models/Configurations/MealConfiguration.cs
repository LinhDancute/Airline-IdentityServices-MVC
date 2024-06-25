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

            //Meal - BoardingPass: n-1
            //TicketClass - Ticket: n-1
            builder.HasMany(bpt => bpt.BoardingPasses)
                .WithOne(tc => tc.Meal)
                .HasForeignKey(bpt => bpt.MealId)
                .IsRequired();

            builder.HasMany(bpt => bpt.Tickets)
                .WithOne(tc => tc.Meal)
                .HasForeignKey(bpt => bpt.MealId)
                .IsRequired();
        }
    }
}
