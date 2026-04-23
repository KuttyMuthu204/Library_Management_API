using Library_Management.DBContext;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_Management.Utilities;

namespace Library_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route(Routes.GetBooks)]
        public async Task<ActionResult<IEnumerable<Books>>> GetBookes()
        {
            var books = await _context.Bookes.ToListAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route(Routes.GetBookById)]
        public async Task<ActionResult<Books>> GetBooks([FromRoute] int id)
        {
            var book = await _context.Bookes.FindAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPut]
        [Route(Routes.UpdateBooks)]
        public async Task<IActionResult> UpdateBooks([FromRoute] int id, [FromBody] Books books)
        {
            if (id != books.BookId)
            {
                return BadRequest();
            }

            _context.Entry(books).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route(Routes.AddBooks)]
        public async Task<ActionResult<Books>> AddBooks(List<Books> books)
        {
            _context.Bookes.AddRange(books);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBooks", new { id = books.FirstOrDefault()?.BookId }, books);
        }


        [HttpDelete]
        [Route(Routes.DeleteBooks)]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            var books = await _context.Bookes.FindAsync(id);

            if (books is null)
            {
                return NotFound();
            }

            _context.Bookes.Remove(books);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BooksExists(int id)
        {
            return _context.Bookes.Any(e => e.BookId == id);
        }
    }
}
