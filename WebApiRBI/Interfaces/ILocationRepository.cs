using WebApiRBI.Models;

namespace WebApiRBI.Interfaces
{
    public interface ILocationRepository
    {
        ICollection<Location> GetLocations();
        Location GetLocation(int locationId);
        Location GetLocation(string city, string street);
        bool LocationExist(int locationId);
        bool CityExist(string city);
        bool CountryExist(string country);
        bool CreateLocation(Location location);
        bool UpdateLocation(Location location);
        bool DeleteLocation(Location location);
        bool Save();
    }
}
