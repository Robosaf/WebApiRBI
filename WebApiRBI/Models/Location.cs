using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRBI.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
