using OdataProj.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataProj.DAL.Repository
{
    public class CategoryRepository : GenericRepository<Product>, ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
