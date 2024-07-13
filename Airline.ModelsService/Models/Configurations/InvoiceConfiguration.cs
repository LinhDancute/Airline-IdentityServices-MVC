using Airline.ModelsService.Models.Statistical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.InvoiceId);

            // User - Invoice : n-1
            builder.HasOne(i => i.Passenger)
                   .WithMany(p => p.Invoices)
                   .HasForeignKey(i => i.PassengerId)
                   .IsRequired();

            // Invoice - InvoiceDetail : 1-1
            builder.HasOne(i => i.InvoiceDetails)
                   .WithOne(d => d.Invoice)
                   .HasForeignKey<InvoiceDetail>(d => d.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
