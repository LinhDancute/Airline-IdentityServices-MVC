using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Airline
{

    public class TicketClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int TicketId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Phải nhập hạng vé")]
        [Display(Name = "Tên hạng vé")]
        public string TicketName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nội dung mô tả hạng vé")]
        public string? Description { set; get; }
        public ICollection<BoardingPass_TicketClass>? BoardingPass_TicketClasses { get; set; }
    }
}