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
            modelBuilder.ApplyConfiguration(new AirportConfiguration());                    //airport
            modelBuilder.ApplyConfiguration(new FlightRouteConfiguration());                //flight route
            modelBuilder.ApplyConfiguration(new FlightRoute_AirportConfiguration());        //flightroute_airport 
            modelBuilder.ApplyConfiguration(new FlightConfiguration());                     //flight
            modelBuilder.ApplyConfiguration(new FlightRoute_FlightConfiguration());         //flightrout_flight
            modelBuilder.ApplyConfiguration(new BoardingPassConfiguration());               //boardingpass
            modelBuilder.ApplyConfiguration(new TicketConfiguration());                     //ticket
            modelBuilder.ApplyConfiguration(new TicketClassConfiguration());                //ticket class
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());                    //invoice
            modelBuilder.ApplyConfiguration(new TicketClass_BaggageConfiguration());        //ticketclass_baggage
            modelBuilder.ApplyConfiguration(new UserConfiguration());                       //user
            modelBuilder.ApplyConfiguration(new MealConfiguration());                       //meal
            modelBuilder.ApplyConfiguration(new BaggageConfiguration());                    //baggage
            //airline
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
        public DbSet<AnnualRevenue> AnnualRevenues { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MonthlyRevenue> MonthlyRevenues { get; set; }
        public DbSet<UnitPrice> UnitPrices { get; set; }
        public DbSet<FlightRoute_Airport> FlightRoute_Airports { get; set; }
        public DbSet<FlightRoute_Flight> FlightRoute_Flights { get; set; }
        public DbSet<TicketClass_Baggage> TicketClass_Baggages { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Baggage> Baggages { get; set; }

    }
}