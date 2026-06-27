using Azure.Security.KeyVault.Secrets;
using Library_Management.Models;
using Library_Management.Services;
using Library_Management.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    /// <summary>
    /// API controller that provides CRUD operations for books.
    /// </summary>
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        private readonly SecretClient _secretClient;

        /// <summary>
        /// Creates a new controller with the provided params.
        /// </summary>
        public LibraryController(ILibraryService libraryService, SecretClient secretClient)
        {
            _libraryService = libraryService;
            _secretClient = secretClient;
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        [HttpGet]
        [Route(Routes.GetBooks)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _libraryService.GetAllBooks(cancellationToken));
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unexpected error occurred while fetching the books");
            }
        }

        /// <summary>
        /// Gets a single book by id.
        /// </summary>
        [HttpGet]
        [Route(Routes.GetBookById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Book>> GetBook([FromRoute][Required] int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _libraryService.GetBookById(id, cancellationToken));
            }
            catch (Exception ex) when (ex is KeyNotFoundException)
            {
                return NotFound($"No book found with this Id: {id}");
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unexpected error occurred while updating the book");
            }
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        [HttpPut]
        [Route(Routes.UpdateBooks)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateBook([FromRoute][Required] int id, [FromBody][Required] Book book, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _libraryService.UpdateBookById(id, book, cancellationToken));
            }
            catch (Exception ex) when (ex is KeyNotFoundException)
            {
                return NotFound($"No book found with this Id: {id}");
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unexpected error occurred while updating the book");
            }
        }

        /// <summary>
        /// Adds a new book.
        /// </summary>
        [HttpPost]
        [Route(Routes.AddBook)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Book>> AddBook([FromBody][Required] Book book, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (await _libraryService.AddBook(book, cancellationToken))
                    return StatusCode(201, book);
                else
                    throw new InvalidOperationException("Failed to add the book. Please try again later.");
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to add the book. Please try again later.");
            }
        }

        /// <summary>
        /// Deletes a book by id.
        /// </summary>
        [HttpDelete]
        [Route(Routes.DeleteBooks)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBook([FromRoute][Required] int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _libraryService.DeleteBook(id, cancellationToken));
            }
            catch (Exception ex) when (ex is KeyNotFoundException)
            {
                return NotFound($"No book found with this Id: {id}");
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unexpected error occurred while deleting the book");
            }
        }

        /// <summary>
        /// Gets DB Connection string from Azure Key Vault and returns all books.
        /// </summary>
        [HttpGet()]
        [Route(Routes.GetConnectionString)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetDBConnection(CancellationToken cancellationToken)
        {
            try
            {
                var connectionString = await _secretClient.GetSecretAsync("Connection");
                return Ok(new { ConnectionString = connectionString });
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unexpected error occurred while getting the secrets.");
            }
        }
    }
}
