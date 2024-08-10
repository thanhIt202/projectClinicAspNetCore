using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Column("CategoryId")]
        [Display(Name = "Category Code")]
        public int CategoryId { get; set; }

        [Column("CategoryName")]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category name cannot be empty")]
        public string? CategoryName { get; set; }

        [Column("CategoryType")]
        [Display(Name = "Category Type")]
        public string? CategoryType { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
