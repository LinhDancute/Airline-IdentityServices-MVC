using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Airline.ModelsService.Models.Configurations;
using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models;
using Airline.ModelsService.Models.Contacts;
using Airline.ModelsService.Models.Statistical;

namespace Airline.ModelsService
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AirportConfiguration());
            modelBuilder.ApplyConfiguration(new FlightRouteConfiguration());
            modelBuilder.ApplyConfiguration(new FlightRoute_AirportConfiguration());
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
            modelBuilder.ApplyConfiguration(new FlightRoute_FlightConfiguration());
            modelBuilder.ApplyConfiguration(new BoardingPassConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TicketClassConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MealConfiguration());
            modelBuilder.ApplyConfiguration(new BaggageConfiguration());
            modelBuilder.ApplyConfiguration(new Ticket_BaggageConfiguration());
            modelBuilder.ApplyConfiguration(new Ticket_MealConfiguration());

            modelBuilder.Entity<Airline.ModelsService.Models.Airline.Airline>(entity =>
            {
                entity.HasKey(a => a.AirlineId);

                entity.HasOne(a => a.ParentAirline)
                    .WithMany(a => a.AirlineChildren)
                    .HasForeignKey(a => a.ParentAirlineId);

                entity.HasMany(a => a.Flights)
                    .WithOne(f => f.Airline)
                    .HasForeignKey(f => f.AirlineId)
                    .IsRequired();
            });
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Airline.ModelsService.Models.Airline.Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<BoardingPass> BoardingPasses { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightRoute> FlightRoutes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketClass> TicketClasses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<UnitPrice> UnitPrices { get; set; }
        public DbSet<FlightRoute_Airport> FlightRoute_Airports { get; set; }
        public DbSet<FlightRoute_Flight> FlightRoute_Flights { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Baggage> Baggages { get; set; }
        public DbSet<Ticket_Meal> Ticket_Meals { get; set; }
        public DbSet<Ticket_Baggage> Ticket_Baggages { get; set; }
    }
}
