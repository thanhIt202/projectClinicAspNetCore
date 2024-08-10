
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        [Column("NewsId")]
        [Display(Name = "News code")]
        public int NewsId { get; set; }

        [Column("Title")]
        [Display(Name = "The Title")]
        [Required(ErrorMessage = "The title cannot be left blank")]
        public string? Title { get; set; }

        [Column("ShortContent")]
        [Display(Name = "Short content")]
        [Required(ErrorMessage = "Short content cannot be empty")]
        public string? ShortContent { get; set; }

        [Column("LongContent")]
        [Display(Name = "Long content")]
        [Required(ErrorMessage = "Long content cannot be empty")]
        public string? LongContent { get; set; }

        [Column("NewsDate")]
        [Display(Name = "News date")]
        public DateTime? NewsDate { get; set; }

        [Column("NewsImage")]
        [Display(Name = "News image")]
        public string? NewsImage { get; set; }

        [Column("NewsType")]
        [Display(Name = "News type")]
        public string? NewsType { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
