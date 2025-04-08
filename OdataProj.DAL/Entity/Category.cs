using System.ComponentModel.DataAnnotations;

namespace OdataProj.DAL
{
    public class Category
    {
        [Key] // Marks Id as the primary key
        public int Id { get; set; }

        [Required] // Ensures Name is not null or empty
        [MaxLength(100)] // Sets a maximum length for Name
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
