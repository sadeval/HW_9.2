using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientAppDb
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }

    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int ClientId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Amount { get; set; }
        public Client Client { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ClientAppDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(c => c.ClientId);

            modelBuilder.Entity<Purchase>()
                .HasKey(p => p.PurchaseId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Purchases)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);
        }
    }

    public class RecentPurchaseDto
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfPurchases { get; set; }
        public double TotalSpent { get; set; }
    }
}