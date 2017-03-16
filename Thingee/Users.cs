using System;
using System.ComponentModel.DataAnnotations;

namespace TheWall
{
    public class User {
         [Required(ErrorMessage = "First Name is required.")]
        [MinLength (2,  ErrorMessage = "First Name must be at least 2 characters.")]
        public string FirstName { get; set; }
         [Required( ErrorMessage = "Last Name is required.")]
        [MinLength (2,  ErrorMessage = "Last Name must be at least 2 characters.")]
        public string LastName { get; set; }
        [Required( ErrorMessage = "E-mail address is required.")]
        [MinLength (6)]
        [EmailAddress( ErrorMessage = "E-mail address is not valid.")]
        public string Email { get; set; }
        // [Required( ErrorMessage = "Username is required.")]
        // [MinLength (2)( ErrorMessage = "Username must be at least 2 characters.")]
        // public string Username { get; set; }
        [Required( ErrorMessage = "Password is required.")]
        [MinLength (8, ErrorMessage = "Password must be at least 8 characters long")]
        // [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9])$", ErrorMessage = "Your Password must contain a Number, a lower Case Letter, and a Capitol Letter.")]
        public string Password { get; set; }
        [Required( ErrorMessage = "Password Confirmation is required.")]
        [Compare("Password", ErrorMessage = "Password and Password Confirmation must match")]
        public string PasswordConfirm {get; set;}
        // public System.DateTime Birthday { get; set; }
        // public System.DateTime HireDate { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
    }
}