using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column("ProductId")]
        [Display(Name = "Product code")]
        public int ProductId { get; set; }

        [Column("ProductName")]
        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Product name cannot be blank")]
        public string? ProductName { get; set; }

        [Column("Price")]
        [Display(Name = "Product price")]
        [DisplayFormat(DataFormatString = "{0:#,0 $}")]
        [Required(ErrorMessage = "Product price cannot be empty")]
        public double? Price { get; set; }

        [Column("Description")]
        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Product description cannot be empty")]
        public string? Description { get; set; }

        [Column("ProductImage")]
        [Display(Name = "Product image")]
        public string? ProductImage { get; set; }

        [Column("ProductStatus")]
        [Display(Name = "Product status")]
        public string? ProductStatus { get; set; }

        [Column("CategoryId")]
        [Display(Name = "Product category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Product category")]
        public Category? Category { get; set; }
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
    }
}
