using System.ComponentModel.DataAnnotations;
namespace TheWall.Models 
{
    public class Message : BaseEntity
    {
        [Key]
        public int MId { get; set; }
        [Required]
        [MinLength (2)]
        public string MessageBody { get; set; }
        [Required]
        public int UserId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
    }
    public class MessageComplete : Message{
        public string FirstName;
    }
    
}