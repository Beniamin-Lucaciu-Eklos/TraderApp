using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Infrastructure.EF
{
    public class TraderDbDesignTimeOptionsFactory
    {
        private readonly string _connectionString;

        public TraderDbDesignTimeOptionsFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TraderDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TraderDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new TraderDbContext(optionsBuilder.Options);
        }
    }
}
