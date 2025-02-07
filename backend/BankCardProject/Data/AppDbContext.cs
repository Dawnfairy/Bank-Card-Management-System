using BankCardProject.DTOs;
using BankCardProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankCardProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ErrorLog>();
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // IDENTITY sütunu
            });


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
