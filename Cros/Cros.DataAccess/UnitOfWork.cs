using Microsoft.EntityFrameworkCore.Storage;

namespace Cros.DataAccess
{
    public class UnitOfWork(CrosDbContext dbContext) : IUnitOfWork
    {
        private readonly CrosDbContext _dbContext = dbContext;

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}