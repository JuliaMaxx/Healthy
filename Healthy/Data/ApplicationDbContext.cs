using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Healthy.Models;

namespace Healthy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Healthy.Models.Food> Food { get; set; } = default!;
        public DbSet<Healthy.Models.Entry> Entry { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between Entry and Food
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.Food)
                .WithMany()
                .HasForeignKey(e => e.FoodId)
                .OnDelete(DeleteBehavior.Cascade); // Use appropriate delete behavior

            // Define the relationship between Entry and IdentityUser
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.IdentityUserId)
                .OnDelete(DeleteBehavior.Restrict); // Use appropriate delete behavior
        }
    }

}
