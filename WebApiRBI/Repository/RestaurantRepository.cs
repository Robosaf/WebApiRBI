using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApiRBI.Data;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataContext _context;

        public RestaurantRepository(DataContext context)
        {
            _context = context;
        }



        public Owner GetOwnerByRestaurantId(int restaurantId)
        {
            return _context.Restaurants
                .Where(r => r.Id == restaurantId).Select(r => r.Owner).OrderBy(o => o.Id).LastOrDefault();
        }

        public Restaurant GetRestaurant(int id)
        {
            return _context.Restaurants.Where(r => r.Id == id).OrderBy(r => r.Id).LastOrDefault();
        }

        public int GetRestaurantRating(int restaurantId)
        {
            var review = _context.Reviews.Where(r => r.Restaurant.Id == restaurantId);

            if (review.Count() <= 0)
            {
                return 0;
            }

            return review.Sum(r => r.Rating) / review.Count();
        }

        public ICollection<Restaurant> GetRestaurants()
        {
            return _context.Restaurants.OrderBy(r => r.Id).ToList();
        }

        public ICollection<Restaurant> GetRestaurantsByCity(string city)
        {
            return _context.Restaurants.Where(r => r.Location.City == city).OrderBy(r => r.Id).ToList();
        }

        public ICollection<Restaurant> GetRestaurantsByCountry(string country)
        {
            return _context.Restaurants.Where(r => r.Location.Country == country).OrderBy(r => r.Id).ToList();
        }

        public ICollection<Restaurant> GetRestaurantsByDelivery(bool hasDelivery)
        {
            return _context.Restaurants.Where(r => r.HasDelivery == hasDelivery).ToList();
        }

        public bool RestaurantExists(int restaurantId)
        {
            return _context.Restaurants.Any(r => r.Id == restaurantId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateRestaurant(Restaurant restaurant)
        {
            _context.Add(restaurant);
            return Save();
        }

        public bool UpdateRestaurant(Restaurant restaurant)
        {
            _context.Update(restaurant);
            return Save();
        }

        public bool DeleteRestaurant(Restaurant restaurant)
        {
            _context.Remove(restaurant);
            return Save();
        }
    }
}
