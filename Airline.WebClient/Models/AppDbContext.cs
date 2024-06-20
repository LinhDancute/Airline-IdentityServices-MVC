using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using App.Models.Contacts;
using App.Models;
using App.Models.Airline;
using App.Models.Configurations;
using App.Models.Statistical;
using Airline.WebClient.Models.Configurations;

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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AirportConfiguration());                    //airport
            modelBuilder.ApplyConfiguration(new AirlineConfiguration());                    //airline
            modelBuilder.ApplyConfiguration(new FlightRouteConfiguration());                //flight route
            modelBuilder.ApplyConfiguration(new FlightRoute_AirportConfiguration());        //flightroute_airport 
            modelBuilder.ApplyConfiguration(new FlightConfiguration());                     //flight
            modelBuilder.ApplyConfiguration(new FlightRoute_FlightConfiguration());         //flightrout_flight
            modelBuilder.ApplyConfiguration(new BoardingPassConfiguration());               //boardingpass
            modelBuilder.ApplyConfiguration(new TicketConfiguration());                     //ticket
            modelBuilder.ApplyConfiguration(new TicketClassConfiguration());                //ticket class
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());                    //invoice
            modelBuilder.ApplyConfiguration(new BoardingPass_TicketClassConfiguration());   //boardingpass_ticketclass
            modelBuilder.ApplyConfiguration(new UserConfiguration());                       //user
        }

        public DbSet<App.Models.Contacts.Contact> Contacts { get; set; }
        public DbSet<App.Models.Airline.Airline> Airlines { get; set; }
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
