using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class BudgetContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryRecord> CategoryRecords { get; set; }

        public DbSet<Record> Records { get; set; }

        public BudgetContext(DbContextOptions<BudgetContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryRecord>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(categoryRecord => categoryRecord.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
