namespace OdataProj.DAL
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Dob {  get; set; } 
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
