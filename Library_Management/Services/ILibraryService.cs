using Library_Management.Models;

namespace Library_Management.Services
{
    public interface ILibraryService
    {
        Task<List<Book>> GetAllBooks(CancellationToken cancellationToken);

        Task<Book?> GetBookById(int id, CancellationToken cancellationToken);

        Task<bool> UpdateBookById(int id, Book book, CancellationToken cancellationToken);

        Task<bool> AddBook(Book book, CancellationToken cancellationToken);

        Task<bool> DeleteBook(int id, CancellationToken cancellationToken);
    }
}
