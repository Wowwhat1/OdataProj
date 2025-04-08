using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OdataProj.DAL
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int Age { get; set; }

        [Column(TypeName = "Date")] // Specifies the data type in the database (optional, but good practice for dates)
        public DateTime Dob { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
