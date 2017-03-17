using System.ComponentModel.DataAnnotations;
namespace TheWall.Models {
    public class Comment : BaseEntity {
        [Key]
        public int CId { get; set; }
        [Required]
        [MinLength (2)]
        public string CommentBody { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MessageId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
    }
    public class CommentComplete : Comment
    {
        public string FirstName;
    }
}