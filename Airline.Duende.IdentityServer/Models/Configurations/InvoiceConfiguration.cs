using Airline.WebClient.Models.Airlines;
using Airline.WebClient.Models.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.WebClient.Models.Configurations {
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>

    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            // DoanhThuThang - HoaDon : n-1
            builder
                .HasOne(pdc => pdc.MonthlyRevenue)
                .WithMany(f => f.Invoices)
                .HasForeignKey(pdc => pdc.MonthlyRevenueId)
                .IsRequired();

            // KhachHang - HoaDon : n-1
            builder.HasOne(kh => kh.Passenger)
                   .WithMany(hd => hd.Invoices)
                   .HasForeignKey(dt => dt.PassengerId)
                   .IsRequired();
        }
    }
}
