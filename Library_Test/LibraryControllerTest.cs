using Azure.Security.KeyVault.Secrets;
using ChustaSoft.Common.Helpers;
using Library_Management.Controllers;
using Library_Management.Models;
using Library_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library_Test
{
    public class LibraryControllerTest
    {
        private readonly Mock<ILibraryService> _libraryServiceMock;
        private readonly LibraryController _libraryController;

        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        private readonly DateTime publishedDate = DateTime.Now;

        public LibraryControllerTest()
        {
            _libraryServiceMock = new Mock<ILibraryService>();
            _libraryController = new LibraryController(_libraryServiceMock.Object);
        }

        [Fact]
        public async Task GetBooks_WithResponse_ShouldReturnBooksList()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.GetAllBooks(_cancellationToken)).ReturnsAsync(GetBooks());

            // Act
            var response = await _libraryController.GetBooks(_cancellationToken);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var books = Assert.IsType<IEnumerable<Book>>(okResult.Value, false);
            Assert.NotEmpty(books);
            Assert.Equal(3, books.Count());

            var result = books.ToArray();

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
        public async Task GetBooks_WithOutResponse_ShouldReturnEmptyList()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.GetAllBooks(_cancellationToken)).ReturnsAsync(new List<Book>());

            // Act
            var response = await _libraryController.GetBooks(_cancellationToken);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var books = Assert.IsType<IEnumerable<Book>>(okResult.Value, false);
            Assert.Empty(books);
        }

        [Fact]
        public async Task GetBooks_WithException_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.GetAllBooks(_cancellationToken)).ThrowsAsync(new Exception("DB Error"));

            // Act & Asseet
            var response = await Assert.ThrowsAsync<InvalidOperationException>(() => _libraryController.GetBooks(_cancellationToken));
            Assert.Equal("Unexpected error occurred while fetching the books", response.Message);
        }

        [Fact]
        public async Task AddBook_WithValidBook_ShouldReturnBook()
        {
            // Arrange
            var book = GetBooks().FirstOrDefault();
            _libraryServiceMock.Setup(c => c.AddBook(book!, _cancellationToken)).ReturnsAsync(true);

            // Act
            var response = await _libraryController.AddBook(book!, _cancellationToken);

            // Assert
            var result = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal(201, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(book, result.Value);
        }

        [Fact]
        public async Task AddBook_WithNullBook_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _libraryController.AddBook(null!, _cancellationToken));
            Assert.Equal("Failed to add the book. Please try again later.", ex.Message);
        }

        [Fact]
        public async Task AddBook_WithInvalidModelState_ShouldThrowBadRequestException()
        {
            // Arrange
            var book = GetBooks().FirstOrDefault();
            _libraryController.ModelState.AddModelError("Title", "Title of the book is required.");

            // Act
            var response = await _libraryController.AddBook(book!, _cancellationToken);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(response.Result);
            var message = Assert.IsType<SerializableError>(result.Value);

            Assert.Equal(400, result.StatusCode);
            Assert.True(message.ContainsKey("Title"));
        }

        [Fact]
        public async Task GetBook_WithValidResponse_ShouldReturnCorrectBook()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.GetBookById(3, _cancellationToken)).ReturnsAsync(GetBooks().LastOrDefault());

            // Act
            var response = await _libraryController.GetBook(3, _cancellationToken);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response.Result);
            var book = Assert.IsType<Book>(result.Value);

            Assert.Equal(3, book.BookId);
            Assert.Equal("Book 3", book.Title);
            Assert.Equal("Author 3", book.Author);
            Assert.Equal(publishedDate, book.PublishedDate, precision: TimeSpan.FromSeconds(10));
            Assert.Equal(4, book.AvailableCopies);
            Assert.Equal(10, book.TotalCopies);
            Assert.Equal("Drama", book.Genre);
            Assert.Equal("Tamil", book.Language);
        }

        [Fact]
        public async Task GetBook_WithInvalidBookId_ShouldThrowNotFoundException()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.GetBookById(3, _cancellationToken)).ThrowsAsync(new KeyNotFoundException("No book found with this Id: 3"));

            // Act
            var response = await _libraryController.GetBook(3, _cancellationToken);

            // Assert
            var result = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No book found with this Id: 3", result.Value);
        }

        [Fact]
        public async Task GetBook_WithException_ShouldThrowException()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.GetBookById(3, _cancellationToken)).ThrowsAsync(new Exception("DB Error"));

            // Act
            var response = await Assert.ThrowsAsync<InvalidOperationException>(() => _libraryController.GetBook(3, _cancellationToken));
            Assert.NotNull(response);
            Assert.Equal("Unexpected error occurred while updating the book", response.Message);
        }

        [Fact]
        public async Task DeleteBook_WithValidResponse_ShouldReturnCorrectResponse()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.DeleteBook(3, _cancellationToken)).ReturnsAsync(true);

            // Act
            var response = await _libraryController.DeleteBook(3, _cancellationToken);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(true, result.Value);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteBook_WithInvalidBookId_ShouldThrowNotFoundException()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.DeleteBook(3, _cancellationToken)).ThrowsAsync(new KeyNotFoundException("No book found with this Id: 3"));

            // Act
            var response = await _libraryController.DeleteBook(3, _cancellationToken);

            // Assert
            var result = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No book found with this Id: 3", result.Value);
        }

        [Fact]
        public async Task DeleteBook_WithException_ShouldThrowException()
        {
            // Arrange
            _libraryServiceMock.Setup(c => c.DeleteBook(3, _cancellationToken)).ThrowsAsync(new Exception("DB Error"));

            // Act
            var response = await Assert.ThrowsAsync<InvalidOperationException>(() => _libraryController.DeleteBook(3, _cancellationToken));
            Assert.NotNull(response);
            Assert.Equal("Unexpected error occurred while deleting the book", response.Message);
        }

        [Fact]
        public async Task UpdateBook_WithValidBook_ShouldReturnOK()
        {
            // Arrange
            var book = GetBooks().FirstOrDefault();
            book!.Title = "Updated Book Title";

            _libraryServiceMock.Setup(c => c.UpdateBookById(book.BookId, book!, _cancellationToken)).ReturnsAsync(true);

            // Act
            var response = await _libraryController.UpdateBook(book.BookId, book!, _cancellationToken);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(true, result.Value);
        }

        [Fact]
        public async Task UpdateBook_WithInvalidModelState_ShouldThrowBadRequestException()
        {
            // Arrange
            var book = GetBooks().FirstOrDefault();
            _libraryController.ModelState.AddModelError("Title", "Title of the book is required.");

            // Act
            var response = await _libraryController.UpdateBook(book!.BookId, book!, _cancellationToken);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(response);
            var message = Assert.IsType<SerializableError>(result.Value);

            Assert.Equal(400, result.StatusCode);
            Assert.True(message.ContainsKey("Title"));
        }

        [Fact]
        public async Task UpdateBook_WithInvalidBookId_ShouldThrowNotFoundException()
        {
            // Arrange
            var book = GetBooks().LastOrDefault();
            _libraryServiceMock.Setup(c => c.UpdateBookById(3, book!, _cancellationToken)).ThrowsAsync(new KeyNotFoundException("No book found with this Id: 3"));

            // Act
            var response = await _libraryController.UpdateBook(3, book!, _cancellationToken);

            // Assert
            var result = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No book found with this Id: 3", result.Value);
        }

        [Fact]
        public async Task UpdateBook_WithException_ShouldThrowException()
        {
            // Arrange
            var book = GetBooks().LastOrDefault();
            _libraryServiceMock.Setup(c => c.UpdateBookById(3, book!, _cancellationToken)).ThrowsAsync(new Exception("DB Error"));

            // Act
            var response = await Assert.ThrowsAsync<InvalidOperationException>(() => _libraryController.UpdateBook(3, book!, _cancellationToken));
            Assert.NotNull(response);
            Assert.Equal("Unexpected error occurred while updating the book", response.Message);
        }

        private List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Author = "Author 1", PublishedDate = publishedDate, AvailableCopies = 5, TotalCopies = 10, Genre = "Drama", Language = "English" },
                new Book { BookId = 2, Title = "Book 2", Author = "Author 2", PublishedDate = publishedDate, AvailableCopies = 3, TotalCopies = 10, Genre = "Drama", Language = "French" },
                new Book { BookId = 3, Title = "Book 3", Author = "Author 3", PublishedDate = publishedDate, AvailableCopies = 4, TotalCopies = 10, Genre = "Drama", Language = "Tamil" }
            };
        }
    }
}
