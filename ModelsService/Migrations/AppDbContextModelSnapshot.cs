﻿// <auto-generated />
using System;
using Airline.ModelsService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Airline", b =>
                {
                    b.Property<int>("AirlineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirlineId"));

                    b.Property<string>("AirlineName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IATAcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ICAOcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentAirlineId")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AirlineId");

                    b.HasIndex("ParentAirlineId");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Airport", b =>
                {
                    b.Property<int>("AirportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirportId"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<string>("AirportName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<int>("Classification")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("AirportId");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Baggage", b =>
                {
                    b.Property<int>("BaggageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BaggageId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BaggageId");

                    b.ToTable("Baggages");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.BoardingPass", b =>
                {
                    b.Property<int>("BoardingPassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoardingPassId"));

                    b.Property<string>("BoardingGate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BoardingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Seat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("BoardingPassId");

                    b.HasIndex("TicketId");

                    b.ToTable("BoardingPasses");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightId"));

                    b.Property<string>("Aircraft")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AirlineId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("ArrivalTime")
                        .IsRequired()
                        .HasColumnType("time");

                    b.Property<int?>("BusinessSeat")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("DepartureTime")
                        .IsRequired()
                        .HasColumnType("time");

                    b.Property<int?>("EconomySeat")
                        .HasColumnType("int");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightSector")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("FlightTime")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<int?>("PremiumEconomySeat")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.HasKey("FlightId");

                    b.HasIndex("AirlineId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.FlightRoute", b =>
                {
                    b.Property<int>("FlightRouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightRouteId"));

                    b.Property<string>("ArrivalAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DepartureAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FlightSector")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightSectorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gate")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("FlightRouteId");

                    b.ToTable("FlightRoutes");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.FlightRoute_Airport", b =>
                {
                    b.Property<int>("FlightRouteID")
                        .HasColumnType("int");

                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.HasKey("FlightRouteID", "AirportID");

                    b.HasIndex("AirportID");

                    b.ToTable("FlightRoute_Airports");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.FlightRoute_Flight", b =>
                {
                    b.Property<int>("FlightRouteID")
                        .HasColumnType("int");

                    b.Property<int>("FlightID")
                        .HasColumnType("int");

                    b.HasKey("FlightRouteID", "FlightID");

                    b.HasIndex("FlightID");

                    b.ToTable("FlightRoute_Flights");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealId"));

                    b.Property<string>("Desciption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MealCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MealId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<string>("BaggageType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("DepartureTime")
                        .HasColumnType("time");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Itinerary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MealRequest")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PNR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassengerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PassengerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassengerPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.Property<string>("Seat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("USD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VND")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketId");

                    b.HasIndex("FlightId");

                    b.HasIndex("PassengerId");

                    b.HasIndex("PriceId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.TicketClass", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FareClass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketId");

                    b.ToTable("TicketClasses");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Ticket_Baggage", b =>
                {
                    b.Property<int>("TicketID")
                        .HasColumnType("int");

                    b.Property<int>("BaggageID")
                        .HasColumnType("int");

                    b.HasKey("TicketID", "BaggageID");

                    b.HasIndex("BaggageID");

                    b.ToTable("Ticket_Baggages");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Ticket_Meal", b =>
                {
                    b.Property<int>("TicketID")
                        .HasColumnType("int");

                    b.Property<int>("MealID")
                        .HasColumnType("int");

                    b.HasKey("TicketID", "MealID");

                    b.HasIndex("MealID");

                    b.ToTable("Ticket_Meals");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CMND")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("HomeAddress")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Contacts.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateSent")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Statistical.AnnualRevenue", b =>
                {
                    b.Property<int>("AnnualRevenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnnualRevenueId"));

                    b.Property<decimal>("Revenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("TicketByYear")
                        .HasColumnType("bigint");

                    b.HasKey("AnnualRevenueId");

                    b.ToTable("AnnualRevenues");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Statistical.Invoice", b =>
                {
                    b.Property<string>("InvoiceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CMND")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MonthlyRevenueId")
                        .HasColumnType("int");

                    b.Property<string>("PassengerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StaffId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("MonthlyRevenueId");

                    b.HasIndex("PassengerId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Statistical.MonthlyRevenue", b =>
                {
                    b.Property<int>("MonthlyRevenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MonthlyRevenueId"));

                    b.Property<int?>("AnnualRevenueId")
                        .HasColumnType("int");

                    b.Property<decimal>("Revenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("TicketByMonth")
                        .HasColumnType("bigint");

                    b.HasKey("MonthlyRevenueId");

                    b.ToTable("MonthlyRevenues");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Statistical.UnitPrice", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceId"));

                    b.Property<decimal>("USD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VND")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PriceId");

                    b.ToTable("UnitPrices");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Airline", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Airline.Airline", "ParentAirline")
                        .WithMany("AirlineChildren")
                        .HasForeignKey("ParentAirlineId");

                    b.Navigation("ParentAirline");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.BoardingPass", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Airline.Ticket", "Ticket")
                        .WithMany("BoardingPasses")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Flight", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Airline.Airline", "Airline")
                        .WithMany("Flights")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.FlightRoute_Airport", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Airline.Airport", "Airport")
                        .WithMany("FlightRoute_Airports")
                        .HasForeignKey("AirportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.Airline.FlightRoute", "FlightRoute")
                        .WithMany("FlightRoute_Airports")
                        .HasForeignKey("FlightRouteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airport");

                    b.Navigation("FlightRoute");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.FlightRoute_Flight", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Airline.Flight", "Flight")
                        .WithMany("FlightRoute_Flights")
                        .HasForeignKey("FlightID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.Airline.FlightRoute", "FlightRoute")
                        .WithMany("FlightRoute_Flights")
                        .HasForeignKey("FlightRouteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("FlightRoute");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Ticket", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Airline.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.AppUser", "Passenger")
                        .WithMany("Tickets")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.Statistical.UnitPrice", "UnitPrice")
                        .WithMany("Tickets")
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.Airline.TicketClass", "TicketClass")
                        .WithMany("Tickets")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("Passenger");

                    b.Navigation("TicketClass");

                    b.Navigation("UnitPrice");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Ticket_Baggage", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Airline.Baggage", "Baggage")
                        .WithMany("Ticket_Baggages")
                        .HasForeignKey("BaggageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.Airline.Ticket", "Ticket")
                        .WithMany("Ticket_Baggages")
                        .HasForeignKey("TicketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Baggage");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Ticket_Meal", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Airline.Meal", "Meal")
                        .WithMany("Ticket_Meals")
                        .HasForeignKey("MealID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.Airline.Ticket", "Ticket")
                        .WithMany("Ticket_Meals")
                        .HasForeignKey("TicketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Statistical.Invoice", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.Statistical.MonthlyRevenue", "MonthlyRevenue")
                        .WithMany("Invoices")
                        .HasForeignKey("MonthlyRevenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.AppUser", "Passenger")
                        .WithMany("Invoices")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonthlyRevenue");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.ModelsService.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Airline.ModelsService.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Airline", b =>
                {
                    b.Navigation("AirlineChildren");

                    b.Navigation("Flights");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Airport", b =>
                {
                    b.Navigation("FlightRoute_Airports");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Baggage", b =>
                {
                    b.Navigation("Ticket_Baggages");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Flight", b =>
                {
                    b.Navigation("FlightRoute_Flights");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.FlightRoute", b =>
                {
                    b.Navigation("FlightRoute_Airports");

                    b.Navigation("FlightRoute_Flights");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Meal", b =>
                {
                    b.Navigation("Ticket_Meals");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.Ticket", b =>
                {
                    b.Navigation("BoardingPasses");

                    b.Navigation("Ticket_Baggages");

                    b.Navigation("Ticket_Meals");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Airline.TicketClass", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.AppUser", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Statistical.MonthlyRevenue", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Airline.ModelsService.Models.Statistical.UnitPrice", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
