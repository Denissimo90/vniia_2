using Microsoft.EntityFrameworkCore;

namespace ReportApp.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
           Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductQty> ProductQties { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
