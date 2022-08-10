using Microsoft.EntityFrameworkCore;
using Task2.Models;

namespace Task2.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        { }
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
    }
}
