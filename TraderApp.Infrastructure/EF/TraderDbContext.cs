using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Models;

namespace TraderApp.Infrastructure.EF
{
    public class TraderDbContext : DbContext
    {
        public TraderDbContext(DbContextOptions<TraderDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AssetTransaction> AssetTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetTransaction>()
                        .OwnsOne(a => a.Asset);

            base.OnModelCreating(modelBuilder);
        }
    }
}
