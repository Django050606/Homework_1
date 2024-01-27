using Homework_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework_1.Data
{
    public class BooksAPIDbContext : DbContext
    {
        public BooksAPIDbContext(DbContextOptions options) : base(options)
        {
        }
    
        public DbSet<Book> Books {  get; set; }
    }
}
