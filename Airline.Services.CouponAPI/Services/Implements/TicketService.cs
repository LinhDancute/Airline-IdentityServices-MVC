using Airline.ModelsService.Models;
using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.ModelsService.Models.Statistical;
using Airline.Services.CouponAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Airline.Services.CouponAPI.Services.Implements
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMealRepository _mealRepository;
        private readonly IBaggageRepository _baggageRepository;
        private readonly ITicketClassRepository _ticketClassRepository;
        private readonly IUnitPriceRepository _unitPriceRepository;
        private readonly IBoardingPassRepository _boardingPassRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        private static readonly Random _random = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public TicketService(ITicketRepository ticketRepository,
                             UserManager<AppUser> userManager,
                             IScheduleRepository scheduleRepository,
                             IMealRepository mealRepository,
                             IBaggageRepository baggageRepository,
                             ITicketClassRepository ticketClassRepository,
                             IUnitPriceRepository unitPriceRepository,
                             IBoardingPassRepository boardingPassRepository,
                             IInvoiceDetailRepository invoiceDetailRepository,
                             IInvoiceRepository invoiceRepository,
                             IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _userManager = userManager;
            _scheduleRepository = scheduleRepository;
            _mealRepository = mealRepository;
            _baggageRepository = baggageRepository;
            _ticketClassRepository = ticketClassRepository;
            _unitPriceRepository = unitPriceRepository;
            _boardingPassRepository = boardingPassRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketDTO>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TicketDTO>>(tickets);
        }

        public async Task<TicketDTO> GetByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new KeyNotFoundException("Ticket not found.");

            return _mapper.Map<TicketDTO>(ticket);
        }

        public async Task UpdateAsync(int id, TicketCreateDTO ticketDTO)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new KeyNotFoundException("Ticket not found.");

            _mapper.Map(ticketDTO, ticket);

            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new KeyNotFoundException("Ticket not found.");

            await _ticketRepository.DeleteAsync(ticket);
        }

        //create single
        public async Task CreateAsync(TicketCreateDTO ticketDTO)
        {
            var ticket = _mapper.Map<TicketCreateDTO, Ticket>(ticketDTO);

            // Validate and get Passenger by Name and PhoneNumber
            var passenger = await GetAppUserByNameAndPhoneAsync(ticketDTO.PassengerName, ticketDTO.PassengerPhoneNumber);
            if (passenger == null)
            {
                throw new Exception($"Passenger with Name {ticketDTO.PassengerName} and Phone Number {ticketDTO.PassengerPhoneNumber} does not exist.");
            }

            // Validate and get Flight by details (Itinerary, FlightNumber, Date, DepartureTime)
            var flight = await GetFlightByDetailsAsync(ticketDTO.Itinerary, ticketDTO.FlightNumber, ticketDTO.Date, ticketDTO.DepartureTime);
            if (flight == null)
            {
                throw new Exception($"Flight with details: Itinerary {ticketDTO.Itinerary}, Flight Number {ticketDTO.FlightNumber}, Date {ticketDTO.Date}, Departure Time {ticketDTO.DepartureTime} does not exist.");
            }

            // Random fare class for ticket with "Economy" or "Business"
            if (ticketDTO.Class == "Economy" || ticketDTO.Class == "Business")
            {
                var matchingClasses = await _ticketClassRepository.GetTicketClassesByNameAsync(ticketDTO.Class);
                if (matchingClasses == null || !matchingClasses.Any())
                {
                    throw new Exception($"No subclasses found for the class {ticketDTO.Class}.");
                }
                ticketDTO.Class = matchingClasses.OrderBy(x => Guid.NewGuid()).Select(tc => tc.TicketName).FirstOrDefault();
            }

            // Validate and get TicketClass by ClassName
            var ticketClass = await _ticketClassRepository.GetTicketClassByNameAsync(ticketDTO.Class);
            if (ticketClass == null)
            {
                throw new Exception($"Ticket class with Name {ticketDTO.Class} does not exist.");
            }

            //update seats count
            if (ticketDTO.Class.Contains("Economy"))
            {
                flight.EconomySeat -= 1;
            }
            else if (ticketDTO.Class.Contains("Business"))
            {
                flight.BusinessSeat -= 1;
            }
            else if (ticketDTO.Class.Contains("Premium Economy"))
            {
                flight.PremiumEconomySeat -= 1;
            }

            decimal usd = decimal.Parse(ticketDTO.USD);
            decimal vnd = string.IsNullOrEmpty(ticketDTO.VND) ? _unitPriceRepository.ConvertUsdToVnd(usd) : decimal.Parse(ticketDTO.VND);

            var unitPrice = await _unitPriceRepository.GetUnitPriceAsync(usd);
            if (unitPrice == null)
            {
                unitPrice = new UnitPrice
                {
                    USD = usd,
                    VND = _unitPriceRepository.ConvertUsdToVnd(usd),
                };

                await _unitPriceRepository.AddAsync(unitPrice);
            }

            ticket.TicketId = await _ticketRepository.GenerateNextTicketIdAsync();

            //set foreign key properties
            ticket.PassengerId = passenger.Id;
            ticket.FlightId = flight.FlightId;
            ticket.PriceId = unitPrice.PriceId;
            ticket.ClassId = ticketClass.TicketClassId;

            //set baggageType with fareclass
            if (ticketDTO.BaggageType == null || !ticketDTO.BaggageType.Any())
            {
                if (ticketDTO.Class.Contains("Economy"))
                {
                    ticketDTO.BaggageType = new List<string> { "Hand baggage", "Baggage calculator", "Free checked baggage" };
                }
                else if (ticketDTO.Class.Contains("Business"))
                {
                    ticketDTO.BaggageType = new List<string> { "Hand baggage", "Baggage calculator", "Free checked baggage", "Excess baggage", "Baggage claim" };
                }
            }

            //set mealRequest with fareclass
            if (ticketDTO.MealRequest == null || !ticketDTO.MealRequest.Any())
            {
                if (ticketDTO.Class.Contains("Business"))
                {
                    var allMeals = await _mealRepository.GetAllAsync();
                    var randomMeals = allMeals.OrderBy(x => Guid.NewGuid()).Take(3).Select(m => m.MealCode).ToList();
                    ticketDTO.MealRequest = randomMeals;
                }
            }

            //random seat with fareclass
            if (string.IsNullOrEmpty(ticketDTO.Seat))
            {
                var random = new Random();
                if (ticketDTO.Class.Contains("Economy"))
                {
                    ticketDTO.Seat = $"{random.Next(15, 25)}{(char)random.Next('A', 'J')}";
                }
                else if (ticketDTO.Class.Contains("Business"))
                {
                    ticketDTO.Seat = $"{random.Next(2, 7)}{(char)random.Next('A', 'E')}";
                }
            }

            // Set other properties
            ticket.PassengerName = passenger.UserName;
            ticket.PassengerPhoneNumber = passenger.PhoneNumber;
            ticket.Itinerary = flight.FlightSector;
            ticket.Date = flight.Date.Date;
            ticket.DepartureTime = flight.DepartureTime;
            ticket.FlightNumber = flight.FlightNumber;
            ticket.Class = ticketClass.TicketName;
            ticket.USD = unitPrice.USD.ToString();
            ticket.VND = unitPrice.VND.ToString();
            ticket.PNR = GeneratePNR();
            ticket.Status = (TicketStatusType)ticketDTO.Status;
            ticket.BaggageType = string.Join(", ", ticketDTO.BaggageType);
            ticket.MealRequest = string.Join(", ", ticketDTO.MealRequest);
            ticket.Seat = ticketDTO.Seat;

            // Add ticket
            await _ticketRepository.AddAsync(ticket);

            // Update seat's quantity in Flight
            await _scheduleRepository.UpdateFlightAsync(flight);

            // Add baggages in ticket_baggage
            foreach (var baggageType in ticketDTO.BaggageType.Where(b => !string.IsNullOrEmpty(b)))
            {
                var baggage = await _baggageRepository.GetBaggageByNameAsync(baggageType);
                if (baggage == null)
                {
                    throw new Exception($"Baggage with name '{baggageType}' does not exist.");
                }

                var ticket_baggage = new Ticket_Baggage
                {
                    BaggageID = baggage.BaggageId,
                    TicketID = ticket.TicketId
                };

                await _ticketRepository.AddTicket_BaggageAsync(ticket_baggage);
            }

            // Add meals in ticket_meal
            foreach (var mealRequest in ticketDTO.MealRequest.Where(m => !string.IsNullOrEmpty(m)))
            {
                var meal = await _mealRepository.GetMealByCodeAsync(mealRequest);
                if (meal == null)
                {
                    throw new Exception($"Meal with code '{mealRequest}' does not exist.");
                }

                var ticket_Meal = new Ticket_Meal
                {
                    MealID = meal.MealId,
                    TicketID = ticket.TicketId
                };

                await _ticketRepository.AddTicket_MealAsync(ticket_Meal);
            }

            // Create BoardingPass
            var boardingPass = new BoardingPass
            {
                TicketId = ticket.TicketId,
                BoardingTime = ticket.DepartureTime - TimeSpan.FromMinutes(30),
                Seat = ticket.Seat
            };

            // Determine BoardingGate based on FlightRoute and GateStatus
            var flightRoute = await _scheduleRepository.GetByFlightSectorAsync(flight.FlightSector);
            if (flightRoute != null)
            {
                if (flightRoute.Gate == FlightRoute.GateStatusType.DomesticGate)
                {
                    boardingPass.BoardingGate = GetRandomGate(6, 11);
                }
                else if (flightRoute.Gate == FlightRoute.GateStatusType.InternationalGate)
                {
                    boardingPass.BoardingGate = GetRandomGate(15, 27);
                }
            }

            // Add BoardingPass
            await _boardingPassRepository.AddAsync(boardingPass);

            // Create invoice
            var invoice = new Invoice
            {
                InvoiceId = Guid.NewGuid().ToString(),
                PassengerId = ticket.PassengerId,
                Date = DateTime.Now,
                Status = InvoiceStatus.Confirmed,
                Passenger = ticket.Passenger
            };

            // Add invoice
            await _invoiceRepository.AddAsync(invoice);

            // Create invoice detail
            var invoiceDetails = new InvoiceDetail
            {
                InvoiceId = invoice.InvoiceId,
                TicketId = ticket.TicketId,
                Class = ticket.Class,
                Itinerary = ticket.Itinerary,
                UnitPrice = ticket.USD,
                Ticket = ticket,
                Invoice = invoice,
            };

            // Add invoice detail
            await _invoiceDetailRepository.AddAsync(invoiceDetails);
        }


        public async Task CreateBulkAsync(IEnumerable<TicketCreateDTO> ticketDTOs, string userId)
        {
            foreach (var ticketDTO in ticketDTOs)
            {
                var ticket = _mapper.Map<TicketCreateDTO, Ticket>(ticketDTO);

                // Validate and get Passenger by Name and PhoneNumber
                var passenger = await GetAppUserByNameAndPhoneAsync(ticketDTO.PassengerName, ticketDTO.PassengerPhoneNumber);
                if (passenger == null)
                {
                    throw new Exception($"Passenger with Name {ticketDTO.PassengerName} and Phone Number {ticketDTO.PassengerPhoneNumber} does not exist.");
                }

                // Validate and get Flight by details (Itinerary, FlightNumber, Date, DepartureTime)
                var flight = await GetFlightByDetailsAsync(ticketDTO.Itinerary, ticketDTO.FlightNumber, ticketDTO.Date, ticketDTO.DepartureTime);
                if (flight == null)
                {
                    throw new Exception($"Flight with details: Itinerary {ticketDTO.Itinerary}, Flight Number {ticketDTO.FlightNumber}, Date {ticketDTO.Date}, Departure Time {ticketDTO.DepartureTime} does not exist.");
                }

                // Random fare class for ticket with "Economy" or "Business"
                if (ticketDTO.Class == "Economy" || ticketDTO.Class == "Business")
                {
                    var matchingClasses = await _ticketClassRepository.GetTicketClassesByNameAsync(ticketDTO.Class);
                    if (matchingClasses == null || !matchingClasses.Any())
                    {
                        throw new Exception($"No subclasses found for the class {ticketDTO.Class}.");
                    }
                    ticketDTO.Class = matchingClasses.OrderBy(x => Guid.NewGuid()).Select(tc => tc.TicketName).FirstOrDefault();
                }

                // Validate and get TicketClass by ClassName
                var ticketClass = await _ticketClassRepository.GetTicketClassByNameAsync(ticketDTO.Class);
                if (ticketClass == null)
                {
                    throw new Exception($"Ticket class with Name {ticketDTO.Class} does not exist.");
                }

                // Update seat counts based on ticket class
                if (ticketDTO.Class.Contains("Economy"))
                {
                    flight.EconomySeat -= 1;
                }
                else if (ticketDTO.Class.Contains("Business"))
                {
                    flight.BusinessSeat -= 1;
                }
                else if (ticketDTO.Class.Contains("Premium Economy"))
                {
                    flight.PremiumEconomySeat -= 1;
                }

                // Convert USD and VND from string to decimal
                decimal usd = decimal.Parse(ticketDTO.USD);
                decimal vnd = string.IsNullOrEmpty(ticketDTO.VND) ? _unitPriceRepository.ConvertUsdToVnd(usd) : decimal.Parse(ticketDTO.VND);

                var unitPrice = await _unitPriceRepository.GetUnitPriceAsync(usd);
                if (unitPrice == null)
                {
                    throw new Exception($"UnitPrice with USD {ticketDTO.USD} and VND {ticketDTO.VND} does not exist.");
                }

                ticket.TicketId = await _ticketRepository.GenerateNextTicketIdAsync();

                // Set foreign key properties
                ticket.PassengerId = passenger.Id;
                ticket.FlightId = flight.FlightId;
                ticket.PriceId = unitPrice.PriceId;
                ticket.ClassId = ticketClass.TicketClassId;

                // Set default for BaggageType
                if (ticketDTO.BaggageType == null || !ticketDTO.BaggageType.Any())
                {
                    if (ticketDTO.Class.Contains("Economy"))
                    {
                        ticketDTO.BaggageType = new List<string> { "Hand baggage", "Baggage calculator", "Free checked baggage" };
                    }
                    else if (ticketDTO.Class.Contains("Business"))
                    {
                        ticketDTO.BaggageType = new List<string> { "Hand baggage", "Baggage calculator", "Free checked baggage", "Excess baggage", "Baggage claim" };
                    }
                }

                // Set default for MealRequest
                if (ticketDTO.MealRequest == null || !ticketDTO.MealRequest.Any())
                {
                    if (ticketDTO.Class.Contains("Business"))
                    {
                        var allMeals = await _mealRepository.GetAllAsync();
                        var randomMeals = allMeals.OrderBy(x => Guid.NewGuid()).Take(3).Select(m => m.MealCode).ToList();
                        ticketDTO.MealRequest = randomMeals;
                    }
                }

                // Random seat assignment based on class
                if (string.IsNullOrEmpty(ticketDTO.Seat))
                {
                    if (ticketDTO.Class.Contains("Economy"))
                    {
                        ticketDTO.Seat = $"{new Random().Next(15, 25)}{(char)new Random().Next('A', 'J')}";
                    }
                    else if (ticketDTO.Class.Contains("Business"))
                    {
                        ticketDTO.Seat = $"{new Random().Next(2, 7)}{(char)new Random().Next('A', 'E')}";
                    }
                }

                // Set other properties
                ticket.PassengerName = passenger.UserName;
                ticket.PassengerPhoneNumber = passenger.PhoneNumber;
                ticket.Itinerary = flight.FlightSector;
                ticket.Date = flight.Date.Date;
                ticket.DepartureTime = flight.DepartureTime;
                ticket.FlightNumber = flight.FlightNumber;
                ticket.Class = ticketClass.TicketName;
                ticket.USD = unitPrice.USD.ToString();
                ticket.VND = unitPrice.VND.ToString();
                ticket.PNR = GeneratePNR();
                ticket.Status = (TicketStatusType)ticketDTO.Status;
                ticket.BaggageType = string.Join(", ", ticketDTO.BaggageType);
                ticket.MealRequest = string.Join(", ", ticketDTO.MealRequest);
                ticket.Seat = ticketDTO.Seat;

                // Add ticket
                await _ticketRepository.AddAsync(ticket);

                // Update seat's quantity in Flight
                await _scheduleRepository.UpdateFlightAsync(flight);

                // Add baggages in ticket_baggage
                foreach (var baggageType in ticketDTO.BaggageType)
                {
                    var baggage = await _baggageRepository.GetBaggageByNameAsync(baggageType);
                    if (baggage == null)
                    {
                        throw new Exception($"Baggage with name '{baggageType}' does not exist.");
                    }

                    var ticket_baggage = new Ticket_Baggage
                    {
                        BaggageID = baggage.BaggageId,
                        TicketID = ticket.TicketId
                    };

                    await _ticketRepository.AddTicket_BaggageAsync(ticket_baggage);
                }

                // Add meals in ticket_meal
                foreach (var mealRequest in ticketDTO.MealRequest)
                {
                    var meal = await _mealRepository.GetMealByCodeAsync(mealRequest);
                    if (meal == null)
                    {
                        throw new Exception($"Meal with code '{mealRequest}' does not exist.");
                    }

                    var ticket_Meal = new Ticket_Meal
                    {
                        MealID = meal.MealId,
                        TicketID = ticket.TicketId
                    };

                    await _ticketRepository.AddTicket_MealAsync(ticket_Meal);
                }

                // Create BoardingPass
                var boardingPass = new BoardingPass
                {
                    TicketId = ticket.TicketId,
                    BoardingTime = ticket.DepartureTime - TimeSpan.FromMinutes(30),
                    Seat = ticket.Seat
                };

                // Determine BoardingGate based on FlightRoute and GateStatus
                var flightRoute = await _scheduleRepository.GetByFlightSectorAsync(flight.FlightSector);
                if (flightRoute != null)
                {
                    if (flightRoute.Gate == FlightRoute.GateStatusType.DomesticGate)
                    {
                        boardingPass.BoardingGate = GetRandomGate(6, 11);
                    }
                    else if (flightRoute.Gate == FlightRoute.GateStatusType.InternationalGate)
                    {
                        boardingPass.BoardingGate = GetRandomGate(15, 27);
                    }
                }

                // Add BoardingPass
                await _boardingPassRepository.AddAsync(boardingPass);

                // Create invoice
                var invoice = new Invoice
                {
                    InvoiceId = Guid.NewGuid().ToString(),
                    PassengerId = ticket.PassengerId,
                    Date = DateTime.Now,
                    Status = InvoiceStatus.Confirmed,
                    Passenger = ticket.Passenger
                };

                // Add invoice
                await _invoiceRepository.AddAsync(invoice);

                // Create invoice detail
                var invoiceDetails = new InvoiceDetail
                {
                    InvoiceId = invoice.InvoiceId,
                    TicketId = ticket.TicketId,
                    Class = ticket.Class,
                    Itinerary = ticket.Itinerary,
                    UnitPrice = ticket.USD,
                    Ticket = ticket,
                    Invoice = invoice,
                };

                // Add invoice detail
                await _invoiceDetailRepository.AddAsync(invoiceDetails);
            }
        }

        // get User's details (username, phoneNumber)
        private async Task<AppUser> GetAppUserByNameAndPhoneAsync(string name, string phoneNumber)
        {
            var users = await _userManager.Users.ToListAsync();
            return users.FirstOrDefault(user => user.UserName == name && user.PhoneNumber == phoneNumber);
        }

        // get Flight's details (flightSector, flightNumber, date, departureTime)
        private async Task<Flight> GetFlightByDetailsAsync(string flightSector, string flightNumber, DateTime date, TimeSpan departureTime)
        {
            var flights = await _scheduleRepository.GetAllFlightAsync();
            return flights.FirstOrDefault(flight => flight.FlightSector == flightSector &&
                                                     flight.FlightNumber == flightNumber &&
                                                     flight.Date == date &&
                                                     flight.DepartureTime == departureTime);
        }

        //generate PNR
        private string GeneratePNR()
        {
            return GenerateRandomCode(6);
        }

        //generate PNR (6 characters)
        private string GenerateRandomCode(int length)
        {
            var code = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                code.Append(_chars[_random.Next(_chars.Length)]);
            }
            return code.ToString();
        }

        //get boardinggate random 
        private string GetRandomGate(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1).ToString();
        }
    }
}
