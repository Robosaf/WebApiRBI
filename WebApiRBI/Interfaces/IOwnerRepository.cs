using WebApiRBI.Models;

namespace WebApiRBI.Interfaces
{
    public interface IOwnerRepository
    {
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwners();
        ICollection<Owner> GetOwnersByLastName(string lastname);
        //Owner GetOwnerByRestaurantId(int restaurantId);
        bool OwnerExist(int ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
        bool Save();
    }
}
