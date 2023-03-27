using WebApiRBI.Data;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext _context;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }
        public Location GetLocation(int locationId)
        {
            return _context.Locations.Where(l => l.Id == locationId).OrderBy(l => l.Id).LastOrDefault();
        }

        public ICollection<Location> GetLocations()
        {
            return _context.Locations.OrderBy(l => l.Id).ToList();
        }

        public bool CityExist(string city)
        {
            return _context.Locations.Any(l => l.City == city);
        }

        public bool LocationExist(int locationId)
        {
            return _context.Locations.Any(l => l.Id == locationId);
        }

        public bool CountryExist(string country)
        {
            return _context.Locations.Any(l => l.Country == country);
        }

        public bool CreateLocation(Location location)
        {
            //we must know about Change Tracker
            _context.Add(location);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public Location GetLocation(string city, string street)
        {
            return _context.Locations
                .Where(l => l.City == city && l.Street == street).LastOrDefault();
        }

        public bool UpdateLocation(Location location)
        {
            _context.Update(location);
            return Save();
        }

        public bool DeleteLocation(Location location)
        {
            _context.Remove(location);
            return Save();
        }
    }
}
