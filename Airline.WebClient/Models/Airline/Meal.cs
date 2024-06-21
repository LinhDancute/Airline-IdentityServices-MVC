using App.Models.Airline;

namespace Airline.WebClient.Models.Airline
{
    public class Meal
    {
        public int MealId { get; set; }
        public string MealCode { get; set; }
        public string Desciption { get; set;}
        public ICollection<BoardingPass>? BoardingPasses { get; } = new List<BoardingPass>();
        public ICollection<Ticket>? Tickets { get; } = new List<Ticket>();
    }
}
