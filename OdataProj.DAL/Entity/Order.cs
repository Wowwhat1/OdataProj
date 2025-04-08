using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OdataProj.DAL
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")] // Specifies User property as the foreign key to the User entity
        public int UserId { get; set; }
        
        public User User { get; set; }

        public double Amount { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
