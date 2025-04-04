namespace OdataProj.DAL
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double Amount { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
