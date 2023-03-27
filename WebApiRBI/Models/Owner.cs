namespace WebApiRBI.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfStart { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
