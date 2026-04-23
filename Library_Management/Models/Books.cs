using Library_Management.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title of the book is required.")]
        [StringLength(50, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author of the book is required.")]
        [StringLength(50, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 10)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Total Copies is required.")]
        [Range(1, 2000, ErrorMessage = "{0} must be between {1} and {2}")]
        public int TotalCopies { get; set; }

        [Required(ErrorMessage = "Availabe Copies is required.")]
        [Range(1, 2000, ErrorMessage = "{0} must be between {1} and {2}")]
        public int AvailableCopies { get; set; }

        [Required(ErrorMessage = "Published Date is required.")]
        [NotInFuture]
        public DateTime PublishedDate { get; set; } = new DateTime();

        [Required(ErrorMessage = "Genere of the book is required.")]
        [StringLength(15, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string Genre { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Language of the book is required.")]
        [StringLength(15, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string Language { get; set; } = string.Empty;
    }
}
