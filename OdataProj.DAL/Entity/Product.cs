using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OdataProj.DAL
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int Price { get; set; }

        [ForeignKey("Category")] // Specifies Category property as the foreign key to the Category entity
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
