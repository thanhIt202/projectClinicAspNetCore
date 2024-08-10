using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project3.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        [Column("CartId")]
        [Display(Name = "Cart code")]
        public int CartId { get; set; }

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

        [Column("AccountId")]
        [Display(Name = "Account")]
        public int? AccountId { get; set; }

        [Display(Name = "Product")]
        public Product? Product { get; set; }

        [Display(Name = "Account")]
        public Account? Account { get; set; }
    }
}
