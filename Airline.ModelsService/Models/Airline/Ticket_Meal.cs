using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.ModelsService.Models.Airline
{
    public class Ticket_Meal
    {
        public int TicketID { set; get; }
        public int MealID { set; get; }


        [ForeignKey("TicketID")]
        public Ticket Ticket { set; get; }

        [ForeignKey("MealID")]
        public Meal Meal { set; get; }
    }
}
