namespace WebApiRBI.Models
{
    public class RestaurantName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebSite { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
