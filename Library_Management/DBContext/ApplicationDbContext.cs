using Library_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.DBContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
                
        }

        public DbSet<Books> Bookes { get; set; }
    }
}
