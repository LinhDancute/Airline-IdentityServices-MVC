using Airline.ModelsService.Models.Statistical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.ModelsService.Models.Configurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasKey(id => id.InvoiceId);

            // Invoice - InvoiceDetail : 1-1
            builder.HasOne(id => id.Invoice)
                   .WithOne(i => i.InvoiceDetails)
                   .HasForeignKey<InvoiceDetail>(id => id.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Ticket - InvoiceDetail : n-1
            builder.HasOne(id => id.Ticket)
                   .WithMany(t => t.InvoiceDetails)
                   .HasForeignKey(id => id.TicketId)
                   .IsRequired(false); 
        }
    }
}
