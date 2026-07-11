using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;
using TraderApp.Infrastructure.EF;

namespace TraderApp.Infrastructure.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly TraderDbDesignTimeOptionsFactory _contextFactory;

        public AccountDataService(TraderDbDesignTimeOptionsFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Account> CreateAsync(Account entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                EntityEntry<Account> createdResult = await context.Set<Account>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                Account entity = await context.Set<Account>().FirstOrDefaultAsync(e => e.Id == id);

                context.Set<Account>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Account> entities = await context.Accounts
                        .Include(a => a.User)
                        .Include(a => a.AssetTransactions)
                        .AsSplitQuery()
                        .ToListAsync();
                return entities;
            }
        }

        public async Task<Account> GetAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                Account entity = await
                    context.Accounts.Include(a => a.User)
                                    .Include(a => a.AssetTransactions)
                                    .AsSplitQuery()
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return entity;
            }
        }

        public async Task<Account> GetByEmail(string email)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                Account entity = await
                    context.Accounts.Include(x => x.User)
                                    .Include(a => a.AssetTransactions)
                                    .AsSplitQuery()
                                    .FirstOrDefaultAsync(e => e.User.Email == email);
                return entity;
            }
        }

        public async Task<Account> GetByUsername(string userName)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                Account entity = await
                    context.Accounts.Include(x => x.User)
                                    .Include(a => a.AssetTransactions)
                                    .AsSplitQuery()
                                    .FirstOrDefaultAsync(e => e.User.UserName == userName);
                return entity;
            }
        }

        public async Task<Account> UpdateAsync(int id, Account entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<Account>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
