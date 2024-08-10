using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [Column("CommentId")]
        [Display(Name = "Comment code")]
        public int CommentId { get; set; }

        [Column("Content")]
        [Display(Name = "Comment Content")]
        public string? Content { get; set; }

        [Column("CommentDate")]
        [Display(Name = "Comment date")]
        public DateTime? CommentDate { get; set; }

        [Column("NewsId")]
        [Display(Name = "News")]
        public int? NewsId { get; set; }

        [Column("AccountId")]
        [Display(Name = "Account")]
        public int? AccountId { get; set; }

        [Display(Name = "News")]
        public News? News { get; set; }

        [Display(Name = "Account")]
        public Account? Account { get; set; }
    }
}
