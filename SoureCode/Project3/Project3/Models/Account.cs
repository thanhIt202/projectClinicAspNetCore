
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [Column("AccountId")]
        [Display(Name = "Account code")]
        public int AccountId { get; set; }

        [Column("FullName")]
        [Display(Name = "Account name")]
        [Required(ErrorMessage = "Account name cannot be blank")]
        public string? FullName { get; set; }

        [Column("Email")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string? Email { get; set; }

        [Column("Password")]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password can not be blank")]
        public string? Password { get; set; }

        [Column("Phone")]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number can not be left blank")]
        [MinLength(10, ErrorMessage = "Phone number must have at least 10 digits")]
        [StringLength(10)]
        [Phone(ErrorMessage = "Phone number format is incorrect")]
        public string? Phone { get; set; }

        [Column("Address")]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address cannot be left blank")]
        public string? Address { get; set; }

        [Column("Avatar")]
        [Display(Name = "Account avatar")]
        public string? Avatar { get; set; }

        [Column("AccountStatus")]
        [Display(Name = "Account status")]
        public string? AccountStatus { get; set; }

        [Column("AccountType")]
        [Display(Name = "Account type")]
        public string? AccountType { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}
