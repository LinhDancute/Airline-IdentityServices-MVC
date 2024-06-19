using Airline.Services.ScheduleAPI.Data.Configurations;
using Airline.Services.ScheduleAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.ScheduleAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Airline.Services.ScheduleAPI.Models.Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightRoute> FlightsRoute { get; set; }
        public DbSet<FlightRoute_Airport> FlightsRoute_Airports { get; set; }
        public DbSet<FlightRoute_Flight> FlightsRoute_Flights { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AirportConfiguration());            //airport
            modelBuilder.ApplyConfiguration(new AirlineConfiguration());            //airline
            modelBuilder.ApplyConfiguration(new FlightRouteConfiguration());        //flight route
            modelBuilder.ApplyConfiguration(new FlightRoute_AirportConfiguration());
            modelBuilder.ApplyConfiguration(new FlightConfiguration());             //flight
            modelBuilder.ApplyConfiguration(new FlightRoute_FlightConfiguration());
        }
    }
}
