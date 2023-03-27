namespace WebApiRBI.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public DateTime DateOfStart { get; set; }
        public string ContactNumber { get; set; }
        public bool HasDelivery { get; set; }
        public Owner Owner { get; set; }
        public RestaurantName RestaurantName { get; set; }
        public Location Location { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
