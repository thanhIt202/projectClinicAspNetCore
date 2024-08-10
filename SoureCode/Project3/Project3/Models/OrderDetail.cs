using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project3.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [Column("OrderDetailId")]
        [Display(Name = "Order Detail code")]
        public int OrderDetailId { get; set; }

        [Column("Quantity")]
        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Column("TotalPrice")]
        [Display(Name = "Total Price")]
        [DisplayFormat(DataFormatString = "{0:#,0 $}")]
        public double? TotalPrice { get; set; }

        [Column("ProductId")]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [Column("OrdersId")]
        [Display(Name = "Orders")]
        public int? OrdersId { get; set; }

        [Display(Name = "Product")]
        public Product? Product { get; set; }

        [Display(Name = "Orders")]
        public Orders? Orders { get; set; }
    }
}
