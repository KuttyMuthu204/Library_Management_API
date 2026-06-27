using Library_Management.DBContext;
using Library_Management.Models;
using Library_Management.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;

namespace Library_Test
{
    public class LibraryServiceTest
    {
        private readonly LibraryService _libraryService;
        private readonly ApplicationDbContext _context;
        private readonly DateTime publishedDate = DateTime.Now;

        public LibraryServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new ApplicationDbContext(options);
            _libraryService = new LibraryService(_context);
        }

        [Fact]
        public async Task GetAllBookes_WithData_ShouldRetrunBooks()
        {
            // Arrange
            await SeedBooks();

            // Act
            var result = await _libraryService.GetAllBooks(CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);

            // Book1
            Assert.Equal(1, result[0].BookId);
            Assert.Equal("Book 1", result[0].Title);
            Assert.Equal("Author 1", result[0].Author);
            Assert.Equal(publishedDate, result[0].PublishedDate, precision: TimeSpan.FromSeconds(10));
            Assert.Equal(5, result[0].AvailableCopies);
            Assert.Equal(10, result[0].TotalCopies);
            Assert.Equal("Drama", result[0].Genre);
            Assert.Equal("English", result[0].Language);

            // Book2
            Assert.Equal(2, result[1].BookId);
            Assert.Equal("Book 2", result[1].Title);
            Assert.Equal("Author 2", result[1].Author);
            Assert.Equal(publishedDate, result[1].PublishedDate, precision: TimeSpan.FromSeconds(10));
            Assert.Equal(3, result[1].AvailableCopies);
            Assert.Equal(10, result[1].TotalCopies);
            Assert.Equal("Drama", result[1].Genre);
            Assert.Equal("French", result[1].Language);

            // Book3
            Assert.Equal(3, result[2].BookId);
            Assert.Equal("Book 3", result[2].Title);
            Assert.Equal("Author 3", result[2].Author);
            Assert.Equal(publishedDate, result[2].PublishedDate, precision: TimeSpan.FromSeconds(10));
            Assert.Equal(4, result[2].AvailableCopies);
            Assert.Equal(10, result[2].TotalCopies);
            Assert.Equal("Drama", result[2].Genre);
            Assert.Equal("Tamil", result[2].Language);
        }

        [Fact]
        public async Task GetAllBookes_WithOutData_ShouldRetrunEmptyList()
        {
            // Act
            var result = await _libraryService.GetAllBooks(CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllBookes_WithNullCancellationToken_ShouldThroughException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("LibraryDB")
               .Options;

            var context = new ApplicationDbContext(options);
            context.Dispose(); // Dispose the context to simulate a database error
            var libraryService = new LibraryService(context);

            // Act
            var ex = await Assert.ThrowsAsync<ObjectDisposedException>(() => libraryService.GetAllBooks(CancellationToken.None));

            // Assert
            Assert.Equal("ApplicationDbContext", ex.ObjectName);
        }

        [Fact]
        public async Task AddBook_WithCorrectData_ShouldReturnTrue()
        {
            // Arrange
            var book = CreateBook();

            // Act
            var response = await _libraryService.AddBook(book, CancellationToken.None);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task AddBook_WithException_ShouldThroughArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _libraryService.AddBook(null!, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteBook_WithInvalidBook_ShouldThroughKeyNotFoundException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _libraryService.DeleteBook(1, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteBook_WithValidBook_ShouldReturnTrue()
        {
            // Arrange
            await SeedBooks();

            // Act
            var response = await _libraryService.DeleteBook(1, CancellationToken.None);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task GetBookById_WithData_ShouldRetrunBook()
        {
            // Arrange
            await SeedBooks();

            // Act
            var result = await _libraryService.GetBookById(1, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

            // Book1
            Assert.Equal(1, result.BookId);
            Assert.Equal("Book 1", result.Title);
            Assert.Equal("Author 1", result.Author);
            Assert.Equal(publishedDate, result.PublishedDate, precision: TimeSpan.FromSeconds(10));
            Assert.Equal(5, result.AvailableCopies);
            Assert.Equal(10, result.TotalCopies);
            Assert.Equal("Drama", result.Genre);
            Assert.Equal("English", result.Language);
        }

        [Fact]
        public async Task GetBookById_WithoutData_ShouldThroughKeyNotFoundException()
        {
            // Arrange
            await SeedBooks();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _libraryService.GetBookById(0, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateBookById_WithSuccessfullUpdate_ShouldRetrunTrue()
        {
            // Arrange
            await SeedBooks();
            var book = new Book
            {
                BookId = 3,
                Title = "Poniyin Selvan",
                Author = "Kalki",
                PublishedDate = publishedDate,
                AvailableCopies = 6,
                TotalCopies = 10,
                Genre = "History",
                Language = "Tamil"
            };

            // Act
            var result = await _libraryService.UpdateBookById(book.BookId, book, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateBookById_WithNoBookFound_ShouldThroughKeyNotFoundException()
        {
            // Arrange
            await SeedBooks();
            var book = CreateBook();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _libraryService.UpdateBookById(book.BookId, book, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateBookById_WithNullBookFound_ShouldThroughArgumentNullException()
        {
            // Arrange
            await SeedBooks();
            var book = CreateBook();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _libraryService.UpdateBookById(book.BookId, null!, CancellationToken.None));
        }

        private async Task SeedBooks()
        {
            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Author = "Author 1", PublishedDate = publishedDate, AvailableCopies = 5, TotalCopies = 10, Genre = "Drama", Language = "English" },
                new Book { BookId = 2, Title = "Book 2", Author = "Author 2", PublishedDate = publishedDate, AvailableCopies = 3, TotalCopies = 10, Genre = "Drama", Language = "French" },
                new Book { BookId = 3, Title = "Book 3", Author = "Author 3", PublishedDate = publishedDate, AvailableCopies = 4, TotalCopies = 10, Genre = "Drama", Language = "Tamil" }
            };

            await _context.Books.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }

        private Book CreateBook()
        {
            return new Book
            {
                BookId = 4,
                Title = "Poniyin Selvan",
                Author = "Kalki",
                PublishedDate = publishedDate,
                AvailableCopies = 6,
                TotalCopies = 10,
                Genre = "History",
                Language = "Tamil"
            };
        }

        //private LibraryService SetupDBException()
        //{
        //    var mock = new Mock<IDBExceptionService>();
        //    mock.Setup(m => m.FindAsync(1, CancellationToken.None)).ReturnsAsync(new Book { BookId = 3, Author = "Author 3" });
        //    mock.Setup(m => m.SaveChangesAsync(CancellationToken.None)).ThrowsAsync(new Exception("DB Error"));
        //    return new LibraryService(mock.Object);
        //}
    }
}
