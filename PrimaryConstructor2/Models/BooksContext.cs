using Microsoft.EntityFrameworkCore;

namespace Scaffolding1.Models;

public class BooksContext(DbContextOptions<BooksContext> options) : DbContext(options)
{
    public DbSet<Book> Book => Set<Book>();
}