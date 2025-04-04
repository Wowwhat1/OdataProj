using Microsoft.EntityFrameworkCore;
using OdataProj.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdataProj.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll() => _dbSet.AsQueryable(); // 🔹 Supports OData Queries

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving entity with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding entity: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating entity: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity == null) throw new Exception($"Entity with ID {id} not found.");

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting entity with ID {id}: {ex.Message}", ex);
            }
        }
    }
}
