using App.Models.Airline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Models.Configurations {
    public class BoardingPassConfiguration : IEntityTypeConfiguration<BoardingPass>

    {
        public void Configure(EntityTypeBuilder<BoardingPass> builder)
        {
            // ChuyenBay - PhieuDatCho : n-1
            builder.HasOne(bp => bp.Flight)
                .WithMany(f => f.BoardingPasses)
                .HasForeignKey(bp => bp.FlightId)
                .IsRequired();

            // KhachHang - PhieuDatCho: n-1
            builder
                .HasOne(pdc => pdc.Passenger)
                .WithMany()
                .HasForeignKey(pdc => pdc.PassengerId)
                .IsRequired();
        }
    }
}
