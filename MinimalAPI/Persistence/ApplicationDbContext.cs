using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entities;

namespace MinimalAPI.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Book> Books{ get; set; }
}