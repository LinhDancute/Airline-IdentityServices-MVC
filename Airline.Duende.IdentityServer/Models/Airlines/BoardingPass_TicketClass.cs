using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.WebClient.Models.Airlines
{

    [Table("BoardingPass_TicketClass")]
    public class BoardingPass_TicketClass
    {
        public int BoardingPassID { set; get; }
        public int TicketClassID { set; get; }


        [ForeignKey("BoardingPassID")]
        public BoardingPass BoardingPass { set; get; }

        [ForeignKey("TicketClassID")]
        public TicketClass TicketClass { set; get; }
    }

}