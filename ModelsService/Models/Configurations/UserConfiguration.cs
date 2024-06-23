using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>

    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // User - Invoice : n-1
            // User - BoardingPass: n-1
            // User - Ticket: n-1
            builder.HasMany(u => u.Invoices)
                .WithOne(i => i.Passenger)
                .HasForeignKey(u => u.PassengerId)
                .IsRequired();

            builder.HasMany(t => t.Tickets)
                .WithOne(i => i.Passenger)
                .HasForeignKey(u => u.PassengerId)
                .IsRequired();

            builder.HasMany(bp => bp.BoardingPasses)
                .WithOne(i => i.Passenger)
                .HasForeignKey(u => u.PassengerId)
                .IsRequired();
        }
    }
}
