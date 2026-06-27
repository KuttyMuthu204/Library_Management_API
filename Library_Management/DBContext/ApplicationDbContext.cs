using Library_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Create a new database context with the supplied options.
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// DbSet representing stored books.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// DbSet representing stored users.
        /// </summary>
        public DbSet<Users> Users { get; set; }
    }
}
