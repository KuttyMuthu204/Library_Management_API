using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    /// <summary>
    /// Represents a user entity stored in the library.
    /// </summary>
    public class Users
    {
        [Required(ErrorMessage = "Firt name of the user is required.")]
        [StringLength(50, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name of the user is required.")]
        [StringLength(50, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string LastName { get; set; } = string.Empty;

        [Key]
        [EmailAddress(ErrorMessage = "Invalid email id format")]
        public string EmailId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string Country { get; set; } = string.Empty;
    }
}
