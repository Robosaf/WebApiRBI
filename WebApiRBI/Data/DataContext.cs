using Microsoft.EntityFrameworkCore;
using WebApiRBI.Models;

namespace WebApiRBI.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) 
           : base(options)
        {

        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantName> RestaurantNames { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MCQUEEN\\SQLEXPRESS;Initial Catalog=WebRestaurants;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
