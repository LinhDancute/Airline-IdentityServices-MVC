namespace Airline.WebClient.Utilities
{
    public class SD
    {
        //auth
        public static string AuthAPIBase {  get; set; }

        //schedule
        public static string AirlineAPIBase { get; set; }
        public static string AirportAPIBase { get; set; }
        public static string FlightAPIBase { get; set; }
        public static string FlightRouteAPIBase { get; set; }
        public static string FlightRoute_AirportAPIBase { get; set; }

        //coupon
        public static string MealAPIBase { get; set; }
        public static string BaggageAPIBase { get; set; }
        public static string UnitPriceAPIBase { get; set; }
        public static string TicketClassAPIBase { get; set; }
        public static string TicketAPIBase { get; set; }
        public static string BoardingPassAPIBase { get; set; }

        public enum ApiType
        {
            GET, POST, PUT, PATCH, DELETE
        }

        public static string SessionToken = "JWTToken";
    }
}
