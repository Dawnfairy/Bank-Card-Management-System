using BankCardProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankCardProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<BankCard> BankCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankCard>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // IDENTITY sütunu
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // IDENTITY sütunu
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
            });


        }
    }
}
