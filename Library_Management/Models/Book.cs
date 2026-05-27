using Library_Management.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management.Models
{
    /// <summary>
    /// Represents a book entity stored in the library.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Primary key for the book.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        /// <summary>
        /// Title of the book.
        /// </summary>
        [Required(ErrorMessage = "Title of the book is required.")]
        [StringLength(50, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Author of the book.
        /// </summary>
        [Required(ErrorMessage = "Author of the book is required.")]
        [StringLength(50, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 10)]
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// Total number of copies the library owns.
        /// </summary>
        [Required(ErrorMessage = "Total Copies is required.")]
        [Range(1, 2000, ErrorMessage = "{0} must be between {1} and {2}")]
        public int TotalCopies { get; set; }

        /// <summary>
        /// Number of copies currently available for loan.
        /// </summary>
        [Required(ErrorMessage = "Availaabe Copies is required.")]
        [Range(1, 2000, ErrorMessage = "{0} must be between {1} and {2}")]
        public int AvailableCopies { get; set; }

        /// <summary>
        /// Date the book was published. Must not be in the future.
        /// </summary>
        [Required(ErrorMessage = "Published Date is required.")]
        [NotInFuture]
        public DateTime PublishedDate { get; set; } = new DateTime();

        /// <summary>
        /// Genre of the book (e.g. Fiction, Science).
        /// </summary>
        [Required(ErrorMessage = "Genre of the book is required.")]
        [StringLength(15, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string Genre { get; set; } = string.Empty;

        /// <summary>
        /// Language the book is written in.
        /// </summary>
        [Required(ErrorMessage = "Language of the book is required.")]
        [StringLength(15, ErrorMessage = "{0} must be between {1} and {2}", MinimumLength = 5)]
        public string Language { get; set; } = string.Empty;
    }
}
