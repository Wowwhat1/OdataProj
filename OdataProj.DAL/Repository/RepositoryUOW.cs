using Microsoft.EntityFrameworkCore;
using OdataProj.DAL.Helper;
using OdataProj.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OdataProj.DAL.Repository
{
    public class RepositoryUOW : IRepositoryUOW
    {
        private readonly DataContext context;
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public RepositoryUOW(IDbContextFactory<DataContext> contextFactory)
        {
            _contextFactory = contextFactory;
            context = _contextFactory.CreateDbContext();
        }

        public IQueryable<Product> GetAll()
        {
            return context.Products.AsNoTracking();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product entity)
        {
            // Configure transaction options:
            // - IsolationLevel.ReadCommitted: ensures that only committed data can be read,
            //   preventing dirty reads from other uncommitted transactions.
            // - Timeout: sets a maximum duration of 2 minutes for the transaction.
            //   If the operation exceeds this time, the transaction will be rolled back automatically.
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(2)
            };
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    await context.Products.AddAsync(entity);
                    await context.SaveChangesAsync();
                    // Save all at once
                    // The Complete method commits the transaction. If an exception has been thrown,
                    // Complete is not called and the transaction is rolled back.
                    scope.Complete();
                    DalHelper.DetachAllEntities(context);
                }
            }
            catch (Exception ex)
            {
                DalHelper.DetachAllEntities(context);
                throw new Exception("An error occurred while adding the product.", ex);
            }
            
        }

        public async Task UpdateAsync(Product entity)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(2)
            };
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    context.Products.Update(entity);
                    await context.SaveChangesAsync();
                    scope.Complete();
                    DalHelper.DetachAllEntities(context);
                }
            }
            catch (Exception ex)
            {
                DalHelper.DetachAllEntities(context);
                throw new Exception("An error occurred while Updating the product.", ex);
            }
         
        }

        public async Task DeleteAsync(int id)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(2)
            };
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var product = await context.Products.FindAsync(id);
                    if (product != null)
                    {
                        context.Products.Remove(product);
                        await context.SaveChangesAsync();
                 
                    }
                    scope.Complete();
                    DalHelper.DetachAllEntities(context);
                }
            }
            catch (Exception ex)
            {
                DalHelper.DetachAllEntities(context);
                throw new Exception("An error occurred while Updating the product.", ex);
            }
        }
    }
}
