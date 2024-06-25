using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.ModelsService.Models.Airline
{
    public class TicketClass_Baggage
    {
        public int TicketClassID { set; get; }
        public int BaggageID { set; get; }


        [ForeignKey("TicketClassID")]
        public TicketClass TicketClass { set; get; }

        [ForeignKey("BaggageID")]
        public Baggage Baggage { set; get; }
    }
}
