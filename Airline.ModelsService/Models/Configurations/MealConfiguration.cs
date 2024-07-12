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

            //Ticket - Ticket_Meal - Meal: n - 1 - n
            builder.HasMany(bpt => bpt.Ticket_Meals)
                .WithOne(tc => tc.Meal)
                .HasForeignKey(bpt => bpt.MealID)
                .IsRequired();
        }
    }
}
