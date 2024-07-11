using Airline.ModelsService.Models.Airline;

namespace Airline.ModelsService.Models.Airline
{
    public class Meal
    {
        public int MealId { get; set; }
        public string MealCode { get; set; }
        public string Desciption { get; set;}
        public ICollection<Ticket_Meal>? Ticket_Meals { get; set; }
    }
}
