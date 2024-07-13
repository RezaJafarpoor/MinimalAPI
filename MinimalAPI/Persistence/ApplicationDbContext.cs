using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entities;

namespace MinimalAPI.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books{ get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>()
            .HasKey(x => x.Isbn);
    }
}