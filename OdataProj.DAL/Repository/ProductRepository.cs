using Microsoft.EntityFrameworkCore;
using OdataProj.DAL.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdataProj.DAL.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetExpensiveProductsAsync(decimal minPrice)
        {
            return await _dbSet.Where(p => p.Price > minPrice).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _dbSet.Where(p => p.Category.Name == category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPagedProductsAsync(int page, int pageSize)
        {
            return await _dbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

    }
}
