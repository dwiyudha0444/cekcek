using Microsoft.EntityFrameworkCore;
using MyNewApp3.Models; // Sesuaikan dengan namespace model

namespace MyNewApp3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definisikan tabel dalam database
        public DbSet<Product> Products { get; set; }
    }
}
