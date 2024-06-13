using Airline.WebClient.Models.Airlines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airline.WebClient.Models.Configurations {
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>

    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            // ChuyenBay - VeMayBay : n-1
            builder.HasOne(bp => bp.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(bp => bp.FlightId)
                .IsRequired();

            // DonGia - VeMayBay : n-1
            builder.HasOne(pdc => pdc.UnitPrice)
                .WithMany()
                .HasForeignKey(pdc => pdc.PriceId)
                .IsRequired();

            // KhachHang - VeMayBay : n-1
            builder.HasOne(pdc => pdc.Passenger)
                .WithMany()
                .HasForeignKey(pdc => pdc.PassengerId)
                .IsRequired();
        }
    }
}
