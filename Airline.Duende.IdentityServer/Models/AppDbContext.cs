using Microsoft.EntityFrameworkCore;
using Airline.WebClient.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Airline.WebClient.Models.Contacts;
using Airline.WebClient.Models.Airlines;
using Airline.WebClient.Models.Statistics;


namespace Airline.WebClient.Models
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

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Airline.WebClient.Models.Airlines.Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<BoardingPass> BoardingPasses { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightRoute> FlightRoutes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketClass> TicketClasses { get; set; }
        public DbSet<AnnualRevenue> AnnualRevenues { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MonthlyRevenue> MonthlyRevenues { get; set; }
        public DbSet<UnitPrice> UnitPrices { get; set; }
        public DbSet<FlightRoute_Airport> FlightRoute_Airports { get; set; }
        public DbSet<FlightRoute_Flight> FlightRoute_Flights { get; set; }
        public DbSet<BoardingPass_TicketClass> BoardingPass_TicketClasses { get; set; }

    }
}
