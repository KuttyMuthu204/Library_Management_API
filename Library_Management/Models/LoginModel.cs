using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models;

public class LoginModel
{
    [Key]
    [EmailAddress(ErrorMessage = "Invalid email id format")]
    public string Username { get; set; } = string.Empty;


    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}
