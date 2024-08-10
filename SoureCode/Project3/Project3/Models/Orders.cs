using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project3.Models
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        [Column("OrdersId")]
        [Display(Name = "Orders code")]
        public int OrdersId { get; set; }

        [Column("ReceiverName")]
        [Display(Name = "Receiver name")]
        [Required(ErrorMessage = "Receiver name cannot be blank")]
        public string? ReceiverName { get; set; }

        [Column("ReceiverPhone")]
        [Display(Name = "Receiver phone")]
        [Required(ErrorMessage = "Receiver phone can not be left blank")]
        [MinLength(10, ErrorMessage = "Receiver phone must have at least 10 digits")]
        [StringLength(10)]
        [Phone(ErrorMessage = "Receiver phone format is incorrect")]
        public string? ReceiverPhone { get; set; }

        [Column("ReceiverAddress")]
        [Display(Name = "Receiver address")]
        [Required(ErrorMessage = "Receiver address be left blank")]
        public string? ReceiverAddress { get; set; }

        [Column("Note")]
        [Display(Name = "Note")]
        public string? Note { get; set; }

        [Column("OrderDate")]
        [Display(Name = "Order date")]
        public DateTime? OrderDate { get; set; }

        [Column("OrderStatus")]
        [Display(Name = "Order Status")]
        public string? OrderStatus { get; set; }

        [Column("AccountId")]
        [Display(Name = "Account")]
        public int? AccountId { get; set; }

        [Display(Name = "Account")]
        public Account? Account { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
