using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataProj.DAL.Repository.Interface
{
    public interface IRepositoryUOW
    {
        IQueryable<Product> GetAll();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(int id);
    }

}
