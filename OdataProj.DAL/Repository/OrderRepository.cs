using OdataProj.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataProj.DAL.Repository
{
    public class OrderRepository : GenericRepository<Product>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context) { }
    }
}
