using Library_Management.Models;

namespace Library_Management.Services
{
    public interface IDBExceptionService
    {
        Task<Book?> FindAsync(int id, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
