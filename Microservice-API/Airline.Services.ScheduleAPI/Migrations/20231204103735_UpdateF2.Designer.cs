﻿// <auto-generated />
using System;
using Airline.Services.ScheduleAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AirlineReservationVietjet.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231204103735_UpdateF2")]
    partial class UpdateF2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("App.Models.Airline.Airline", b =>
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

                    b.ToTable("Airline");
                });

            modelBuilder.Entity("App.Models.Airline.Airport", b =>
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

            modelBuilder.Entity("App.Models.Airline.BoardingPass", b =>
                {
                    b.Property<int>("BoardingPassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoardingPassId"));

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CMND")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<string>("PassengerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Seat")
                        .HasColumnType("int");

                    b.HasKey("BoardingPassId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("FlightId");

                    b.HasIndex("PassengerId");

                    b.ToTable("BoardingPasses");
                });

            modelBuilder.Entity("App.Models.Airline.BoardingPass_TicketClass", b =>
                {
                    b.Property<int>("BoardingPassID")
                        .HasColumnType("int");

                    b.Property<int>("TicketClassID")
                        .HasColumnType("int");

                    b.HasKey("BoardingPassID", "TicketClassID");

                    b.HasIndex("TicketClassID");

                    b.ToTable("BoardingPass_TicketClasses", (string)null);
                });

            modelBuilder.Entity("App.Models.Airline.Flight", b =>
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

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeluxeSeat")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("DepartureTime")
                        .IsRequired()
                        .HasColumnType("time");

                    b.Property<int?>("EcoSeat")
                        .HasColumnType("int");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightSector")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("FlightTime")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<int?>("SkyBossBusinessSeat")
                        .HasColumnType("int");

                    b.Property<int?>("SkyBossSeat")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.HasKey("FlightId");

                    b.HasIndex("AirlineId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("App.Models.Airline.FlightRoute", b =>
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

            modelBuilder.Entity("App.Models.Airline.FlightRoute_Airport", b =>
                {
                    b.Property<int>("FlightRouteID")
                        .HasColumnType("int");

                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.HasKey("FlightRouteID", "AirportID");

                    b.HasIndex("AirportID");

                    b.ToTable("FlightRoute_Airports", (string)null);
                });

            modelBuilder.Entity("App.Models.Airline.FlightRoute_Flight", b =>
                {
                    b.Property<int>("FlightRouteID")
                        .HasColumnType("int");

                    b.Property<int>("FlightID")
                        .HasColumnType("int");

                    b.HasKey("FlightRouteID", "FlightID");

                    b.HasIndex("FlightID");

                    b.ToTable("FlightRoute_Flights", (string)null);
                });

            modelBuilder.Entity("App.Models.Airline.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CMND")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<string>("PassengerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<int?>("UnitPricePriceId")
                        .HasColumnType("int");

                    b.HasKey("TicketId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("FlightId");

                    b.HasIndex("PassengerId");

                    b.HasIndex("PriceId");

                    b.HasIndex("UnitPricePriceId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("App.Models.Airline.TicketClass", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TicketId");

                    b.ToTable("TicketClasses");
                });

            modelBuilder.Entity("App.Models.AppUser", b =>
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

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("App.Models.Contacts.Contact", b =>
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

            modelBuilder.Entity("App.Models.Staff.Staff", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("App.Models.Staff.StaffRole", b =>
                {
                    b.Property<string>("StaffId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StaffId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("StaffRoles", (string)null);
                });

            modelBuilder.Entity("App.Models.Statistical.AnnualRevenue", b =>
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

            modelBuilder.Entity("App.Models.Statistical.Invoice", b =>
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("MonthlyRevenueId");

                    b.HasIndex("PassengerId");

                    b.HasIndex("StaffId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("App.Models.Statistical.MonthlyRevenue", b =>
                {
                    b.Property<int>("MonthlyRevenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MonthlyRevenueId"));

                    b.Property<int>("AnnualRevenueId")
                        .HasColumnType("int");

                    b.Property<decimal>("Revenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("TicketByMonth")
                        .HasColumnType("bigint");

                    b.HasKey("MonthlyRevenueId");

                    b.ToTable("MonthlyRevenues");
                });

            modelBuilder.Entity("App.Models.Statistical.UnitPrice", b =>
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

                    b.ToTable("Roles", (string)null);
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

                    b.ToTable("RoleClaims", (string)null);
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

                    b.ToTable("UserClaims", (string)null);
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

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
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

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("App.Models.Airline.Airline", b =>
                {
                    b.HasOne("App.Models.Airline.Airline", "ParentAirline")
                        .WithMany("AirlineChildren")
                        .HasForeignKey("ParentAirlineId");

                    b.Navigation("ParentAirline");
                });

            modelBuilder.Entity("App.Models.Airline.BoardingPass", b =>
                {
                    b.HasOne("App.Models.AppUser", null)
                        .WithMany("BoardingPasses")
                        .HasForeignKey("AppUserId");

                    b.HasOne("App.Models.Airline.Flight", "Flight")
                        .WithMany("BoardingPasses")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.AppUser", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("App.Models.Airline.BoardingPass_TicketClass", b =>
                {
                    b.HasOne("App.Models.Airline.BoardingPass", "BoardingPass")
                        .WithMany()
                        .HasForeignKey("BoardingPassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.Airline.TicketClass", "TicketClass")
                        .WithMany()
                        .HasForeignKey("TicketClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoardingPass");

                    b.Navigation("TicketClass");
                });

            modelBuilder.Entity("App.Models.Airline.Flight", b =>
                {
                    b.HasOne("App.Models.Airline.Airline", "Airline")
                        .WithMany("Flights")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");
                });

            modelBuilder.Entity("App.Models.Airline.FlightRoute_Airport", b =>
                {
                    b.HasOne("App.Models.Airline.Airport", "Airport")
                        .WithMany("FlightRoute_Airports")
                        .HasForeignKey("AirportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.Airline.FlightRoute", "FlightRoute")
                        .WithMany("FlightRoute_Airports")
                        .HasForeignKey("FlightRouteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airport");

                    b.Navigation("FlightRoute");
                });

            modelBuilder.Entity("App.Models.Airline.FlightRoute_Flight", b =>
                {
                    b.HasOne("App.Models.Airline.Flight", "Flight")
                        .WithMany("FlightRoute_Flights")
                        .HasForeignKey("FlightID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.Airline.FlightRoute", "FlightRoute")
                        .WithMany("FlightRoute_Flights")
                        .HasForeignKey("FlightRouteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("FlightRoute");
                });

            modelBuilder.Entity("App.Models.Airline.Ticket", b =>
                {
                    b.HasOne("App.Models.AppUser", null)
                        .WithMany("Tickets")
                        .HasForeignKey("AppUserId");

                    b.HasOne("App.Models.Airline.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.AppUser", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.Statistical.UnitPrice", "UnitPrice")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.Statistical.UnitPrice", null)
                        .WithMany("Tickets")
                        .HasForeignKey("UnitPricePriceId");

                    b.Navigation("Flight");

                    b.Navigation("Passenger");

                    b.Navigation("UnitPrice");
                });

            modelBuilder.Entity("App.Models.Staff.StaffRole", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.Staff.Staff", "Staff")
                        .WithMany("StaffRoles")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("App.Models.Statistical.Invoice", b =>
                {
                    b.HasOne("App.Models.Statistical.MonthlyRevenue", "MonthlyRevenue")
                        .WithMany("Invoices")
                        .HasForeignKey("MonthlyRevenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.AppUser", "Passenger")
                        .WithMany("Invoices")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.Staff.Staff", "Staff")
                        .WithMany("Invoices")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonthlyRevenue");

                    b.Navigation("Passenger");

                    b.Navigation("Staff");
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
                    b.HasOne("App.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("App.Models.AppUser", null)
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

                    b.HasOne("App.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("App.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Models.Airline.Airline", b =>
                {
                    b.Navigation("AirlineChildren");

                    b.Navigation("Flights");
                });

            modelBuilder.Entity("App.Models.Airline.Airport", b =>
                {
                    b.Navigation("FlightRoute_Airports");
                });

            modelBuilder.Entity("App.Models.Airline.Flight", b =>
                {
                    b.Navigation("BoardingPasses");

                    b.Navigation("FlightRoute_Flights");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("App.Models.Airline.FlightRoute", b =>
                {
                    b.Navigation("FlightRoute_Airports");

                    b.Navigation("FlightRoute_Flights");
                });

            modelBuilder.Entity("App.Models.AppUser", b =>
                {
                    b.Navigation("BoardingPasses");

                    b.Navigation("Invoices");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("App.Models.Staff.Staff", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("StaffRoles");
                });

            modelBuilder.Entity("App.Models.Statistical.MonthlyRevenue", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("App.Models.Statistical.UnitPrice", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
