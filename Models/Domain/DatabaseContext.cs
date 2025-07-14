using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Analytics_Hub.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PythonScript> PythonScripts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-one relationship between Category and PythonScript
            modelBuilder.Entity<Category>()
                .HasOne(c => c.PythonScript)
                .WithOne(p => p.Category)
                .HasForeignKey<PythonScript>(p => p.CategoryId);

            // Add other configurations as needed

            base.OnModelCreating(modelBuilder);
        }
    }
}
