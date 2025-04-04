using OdataProj.DAL.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdataProj.DAL.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetExpensiveProductsAsync(decimal minPrice)
        {
            return await Task.FromResult(_context.Products.Where(p => p.Price > minPrice).ToList());
        }
    }
}
