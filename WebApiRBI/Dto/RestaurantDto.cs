using WebApiRBI.Models;

namespace WebApiRBI.Dto
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public DateTime DateOfStart { get; set; }
        public string ContactNumber { get; set; }
        public bool HasDelivery { get; set; }
    }
}
