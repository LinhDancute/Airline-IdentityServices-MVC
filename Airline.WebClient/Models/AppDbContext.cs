using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using App.Models.Contacts;
using App.Models;
using App.Models.Airline;
using App.Models.Configurations;
using App.Models.Statistical;

namespace App.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration, ILogger<AppDbContext> logger)
            : base(options)
        {
            _configuration = configuration;
            _logger = logger;

            // Enable logging
            this.Database.SetCommandTimeout(150); // Optionally set command timeout
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            string connectionString = _configuration.GetConnectionString("AirlineReservationDb");
            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new FlightConfiguration());
            modelBuilder.ApplyConfiguration(new BoardingPassConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());

            modelBuilder.Entity<FlightRoute_Airport>(e =>
            {
                e.ToTable("FlightRoute_Airports");
                e.HasKey(pc => new { pc.FlightRouteID, pc.AirportID });
            });

            modelBuilder.Entity<FlightRoute_Flight>(e =>
            {
                e.ToTable("FlightRoute_Flights");
                e.HasKey(pc => new { pc.FlightRouteID, pc.FlightID });
            });

            modelBuilder.Entity<BoardingPass_TicketClass>(e =>
            {
                e.ToTable("BoardingPass_TicketClasses");
                e.HasKey(pc => new { pc.BoardingPassID, pc.TicketClassID });
            });
        }

        public DbSet<App.Models.Contacts.Contact> Contacts { get; set; }
        public DbSet<App.Models.Airline.Airline> Airlines { get; set; }
        public DbSet<App.Models.Airline.Airport> Airports { get; set; }
        public DbSet<App.Models.Airline.BoardingPass> BoardingPasses { get; set; }
        public DbSet<App.Models.Airline.Flight> Flights { get; set; }
        public DbSet<App.Models.Airline.FlightRoute> FlightRoutes { get; set; }
        public DbSet<App.Models.Airline.Ticket> Tickets { get; set; }
        public DbSet<App.Models.Airline.TicketClass> TicketClasses { get; set; }
        public DbSet<App.Models.Statistical.AnnualRevenue> AnnualRevenues { get; set; }
        public DbSet<App.Models.Statistical.Invoice> Invoices { get; set; }
        public DbSet<App.Models.Statistical.MonthlyRevenue> MonthlyRevenues { get; set; }
        public DbSet<App.Models.Statistical.UnitPrice> UnitPrices { get; set; }
        public DbSet<App.Models.Airline.FlightRoute_Airport> FlightRoute_Airports { get; set; }
        public DbSet<App.Models.Airline.FlightRoute_Flight> FlightRoute_Flights { get; set; }
        public DbSet<App.Models.Airline.BoardingPass_TicketClass> BoardingPass_TicketClasses { get; set; }

    }
}
