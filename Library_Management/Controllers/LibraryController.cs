using Library_Management.DBContext;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_Management.Utilities;
using System.Data;

namespace Library_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// API controller that provides CRUD operations for books.
    /// </summary>
    public class LibraryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Creates a new controller with the provided database context.
        /// </summary>
        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        [HttpGet]
        [Route(Routes.GetBooks)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(CancellationToken cancellationToken)
        {
            var books = await _context.Books.ToListAsync(cancellationToken);
            return Ok(books);
        }

        /// <summary>
        /// Gets a single book by id.
        /// </summary>
        [HttpGet]
        [Route(Routes.GetBookById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Book>> GetBook([FromRoute] int id, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(id, cancellationToken);

            if (book is null)
            {
                return Problem(detail: $"Book with ID {id} not found.", statusCode: 404);
            }

            return Ok(book);
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        [HttpPut]
        [Route(Routes.UpdateBooks)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] Book books, CancellationToken cancellationToken)
        {
            if (id != books.BookId)
                return BadRequest($"The Route parameter Id is not matching with Entity Id");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBook = await _context.Books.FindAsync(new object[] { id }, cancellationToken);

            if (existingBook is null)
                return NotFound($"Book with ID {id} not found.");

            existingBook.BookId = books.BookId;
            existingBook.Title = books.Title;
            existingBook.Author = books.Author;
            existingBook.TotalCopies = books.TotalCopies;
            existingBook.AvailableCopies = books.AvailableCopies;
            existingBook.PublishedDate = books.PublishedDate;
            existingBook.Genre = books.Genre;
            existingBook.Language = books.Language;

            try
            {
                _context.Books.Update(existingBook);
                await _context.SaveChangesAsync(cancellationToken);
                return NoContent();
            }
            catch (DBConcurrencyException)
            {
                if (!await BookExistsAsync(id, cancellationToken)) return NotFound();
                throw;
            }
        }

        /// <summary>
        /// Adds a new book.
        /// </summary>
        [HttpPost]
        [Route(Routes.AddBook)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Books.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return StatusCode(201, book);
        }


        /// <summary>
        /// Deletes a book by id.
        /// </summary>
        [HttpDelete]
        [Route(Routes.DeleteBooks)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBook([FromRoute] int id, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(id, cancellationToken);

            if (book is null)
                return NotFound($"Book with ID {id} not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Check exiatance of a book by id.
        /// </summary>
        private async Task<bool> BookExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Books.AnyAsync(e => e.BookId == id, cancellationToken);
        }
    }
}
