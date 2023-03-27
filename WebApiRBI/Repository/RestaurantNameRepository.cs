using WebApiRBI.Data;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Repository
{
    public class RestaurantNameRepository : IRestaurantNameRepository
    {
        private readonly DataContext _context;

        public RestaurantNameRepository(DataContext context)
        {
            _context = context;
        }

        public RestaurantName GetRestName(int nameId)
        {
            return _context.RestaurantNames.Where(rn => rn.Id == nameId).FirstOrDefault();
        }

        public RestaurantName GetRestNameByName(string name)
        {
            return _context.RestaurantNames.Where(rn => rn.Name == name).FirstOrDefault();
        }

        public ICollection<RestaurantName> GetRestNames()
        {
            return _context.RestaurantNames.OrderBy(rn => rn.Id).ToList();
        }
        public bool RestNameExist(int nameId)
        {
            return _context.RestaurantNames.Any(rn => rn.Id == nameId);
        }
        public bool RestNameExist(string name)
        {
            return _context.RestaurantNames.Any(rn => rn.Name == name);
        }
        public bool CreateRestaurantName(RestaurantName restName)
        {
            _context.Add(restName);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRestaurantName(RestaurantName restName)
        {
            _context.Update(restName);
            return Save();
        }

        public bool DeleteRestaurantName(RestaurantName restName)
        {
            _context.Remove(restName);
            return Save();
        }
    }
}
