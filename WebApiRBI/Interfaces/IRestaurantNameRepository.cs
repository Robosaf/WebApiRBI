using WebApiRBI.Models;

namespace WebApiRBI.Interfaces
{
    public interface IRestaurantNameRepository
    {
        RestaurantName GetRestName(int nameId);
        ICollection<RestaurantName> GetRestNames();
        RestaurantName GetRestNameByName(string name);
        bool RestNameExist(int nameId);
        bool RestNameExist(string name);
        bool CreateRestaurantName(RestaurantName restName);
        bool UpdateRestaurantName(RestaurantName restName);
        bool DeleteRestaurantName(RestaurantName restName);
        bool Save();

    }
}
