using Library_Management.DBContext;
using Library_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Services
{
    public class LibraryService : ILibraryService
    {
        private ApplicationDbContext _context;

        public LibraryService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Book>> GetAllBooks(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Books.AsNoTracking().ToListAsync(cancellationToken) ?? new List<Book>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddBook(Book book, CancellationToken cancellationToken)
        {
            try
            {
                if (book is null)
                    throw new ArgumentNullException(nameof(book));

                await _context.Books.AddAsync(book, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteBook(int id, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _context.Books.FindAsync(id, cancellationToken);

                if (book is null)
                    throw new KeyNotFoundException($"No book found with this Id: {id}");
                else
                    _context.Books.Remove(book);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book?> GetBookById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _context.Books.FindAsync(new object[] { id }, cancellationToken);

                if (book is null)
                    throw new KeyNotFoundException($"No book found with this Id: {id}");
                else
                    return book;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBookById(int id, Book book, CancellationToken cancellationToken)
        {
            try
            {
                if (book is null)
                    throw new ArgumentNullException(nameof(book));

                var existingBook = await _context.Books.FindAsync(new object[] { id }, cancellationToken);

                if (existingBook is null)
                    throw new KeyNotFoundException($"No book found with this Id: {id}");

                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.TotalCopies = book.TotalCopies;
                existingBook.AvailableCopies = book.AvailableCopies;
                existingBook.PublishedDate = book.PublishedDate;
                existingBook.Genre = book.Genre;
                existingBook.Language = book.Language;

                _context.Books.Update(existingBook);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
