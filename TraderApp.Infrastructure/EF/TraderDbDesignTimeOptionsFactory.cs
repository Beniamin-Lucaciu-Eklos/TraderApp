using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Infrastructure.EF
{
    public class TraderDbDesignTimeOptionsFactory : IDesignTimeDbContextFactory<TraderDbContext>
    {
        public TraderDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TraderDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TraderDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Command Timeout=0");
            return new TraderDbContext(optionsBuilder.Options);
        }
    }
}
