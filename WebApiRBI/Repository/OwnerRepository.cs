using Microsoft.EntityFrameworkCore;
using WebApiRBI.Data;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        private readonly IRestaurantRepository _restaurantRepository;

        public OwnerRepository(DataContext context, IRestaurantRepository restaurantRepository)
        {
            _context = context;
            _restaurantRepository = restaurantRepository;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).OrderBy(o => o.Id).FirstOrDefault();
        }

        //public Owner GetOwnerByRestaurantId(int restaurantId)
        //{
        //    var restaurant = _restaurantRepository.GetRestaurant(restaurantId);
        //    var ownerId = restaurant.Owner.Id;

        //    return GetOwner(ownerId);
        //}
        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(o => o.Id).ToList();
        }

        public ICollection<Owner> GetOwnersByLastName(string lastname)
        {
            return _context.Owners.Where(o => o.LastName == lastname).ToList();
        }

        public bool OwnerExist(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }
    }
}
