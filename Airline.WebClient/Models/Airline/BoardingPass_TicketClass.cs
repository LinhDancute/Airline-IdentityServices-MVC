using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Airline
{
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