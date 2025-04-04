using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OdataProj.DAL;

namespace OdataProj.Controllers
{
    public class UserController : ODataController
    {
        private static Random random = new Random();
        private static List<User> customers = new List<User>(
            Enumerable.Range(1, 3).Select(idx => new User
            {
                Id = idx,
                Name = $"Customer {idx}",
                Orders = new List<Order>(
                    Enumerable.Range(1, 2).Select(dx => new Order
                    {
                        Id = (idx - 1) * 2 + dx,
                        Amount = random.Next(1, 9) * 10
                    }))
            }));

        [EnableQuery]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(customers);
        }

        [EnableQuery]
        public ActionResult<User> Get([FromRoute] int key)
        {
            var item = customers.SingleOrDefault(d => d.Id.Equals(key));

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
