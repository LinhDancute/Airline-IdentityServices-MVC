using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.ModelsService.Models.Airline
{
    public class Ticket_Baggage
    {
        public int TicketID { set; get; }
        public int BaggageID { set; get; }


        [ForeignKey("TicketID")]
        public Ticket Ticket { set; get; }

        [ForeignKey("BaggageID")]
        public Baggage Baggage { set; get; }
    }
}
