using Airline.ModelsService.Models.Airline;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Airline
{

    public class TicketClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int TicketId { get; set; }

        [Display(Name = "Tên hạng vé")]
        public string TicketName { get; set; }

        [Display(Name = "Mã hạng vé")]
        public string FareClass { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nội dung mô tả hạng vé")]
        public string? Description { set; get; }

        [NotMapped]
        [Display(Name = "Hạng vé")]
        public string Status
        {
            get
            {
                if (new[] { "B", "M" }.Contains(FareClass)) return "Economy Flex";
                if (new[] { "S", "H", "K", "L" }.Contains(FareClass)) return "Economy Classic";
                if (new[] { "Q", "N", "R", "T", "E" }.Contains(FareClass)) return "Economy Lite";
                if (new[] { "P", "A", "G" }.Contains(FareClass)) return "Economy Super Lite";
                if (FareClass == "W") return "Premium Economy Flex";
                if (new[] { "Z", "U" }.Contains(FareClass)) return "Premium Economy Classic";
                if (new[] { "J", "C" }.Contains(FareClass)) return "Business Flex";
                if (new[] { "D", "I" }.Contains(FareClass)) return "Business Classic";
                return "Unknown";
            }
        }

        public ICollection<Ticket>? Tickets { get; } = new List<Ticket>();
        public ICollection<BoardingPass>? BoardingPasses { get; } = new List<BoardingPass>();
        public ICollection<TicketClass_Baggage>? TicketClass_Baggages { get; set; } = new List<TicketClass_Baggage>();
    }
}