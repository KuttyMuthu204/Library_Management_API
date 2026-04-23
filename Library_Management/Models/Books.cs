using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class Books
    {
        [Key]
        public int BookId{ get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int TotalCopies { get; set; }

        public int AvailableCpoies { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Genre { get; set; } = string.Empty;

        public Guid ISBN { get; set; } = new Guid();

        public string Language { get; set; } = string.Empty;
    }
}
