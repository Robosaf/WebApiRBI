using WebApiRBI.Models;

namespace WebApiRBI.Interfaces
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int restaurantId);
        ICollection<Restaurant> GetRestaurantsByDelivery(bool hasDelivery);
        ICollection<Restaurant> GetRestaurantsByCity(string city);
        ICollection<Restaurant> GetRestaurantsByCountry(string country);
        Owner GetOwnerByRestaurantId(int restaurantId);
        bool RestaurantExists(int restaurantId);
        int GetRestaurantRating(int restaurantId);
        //ICollection<Restaurant> GetRestaurantsGTEnterRating(int rating);
        bool CreateRestaurant(Restaurant restaurant);
        bool UpdateRestaurant(Restaurant restaurant);
        bool DeleteRestaurant(Restaurant restaurant);
        bool Save();
    }
}
