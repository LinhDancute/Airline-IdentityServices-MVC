namespace Airline.WebClient.Utilities
{
    public class SD
    {
        public static string TicketClassAPIBase { get; set; }
        public static string AirlineAPIBase { get; set; }
        public static string AirportAPIBase { get; set; }
        public static string FlightAPIBase { get; set; }
        public static string FlightRouteAPIBase { get; set; }
        public static string FlightRoute_AirportAPIBase { get; set; }
        public enum ApiType
        {
            GET, POST, PUT, PATCH, DELETE
        }
    }
}
