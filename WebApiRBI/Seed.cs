using WebApiRBI.Data;
using WebApiRBI.Models;

namespace WebApiRBI
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Restaurants.Any())
            {
                var restaurants = new List<Restaurant>()
                {
                    new Restaurant()
                    {
                        DateOfStart = new DateTime(2013, 2, 3),
                        ContactNumber = "+48_345_936_693",
                        HasDelivery = false,
                        Owner = new Owner()
                        {
                            FirstName = "Georgi",
                            LastName = "Abramov",
                            DateOfStart = new DateTime(2018, 4, 6),
                            ContactNumber = "+48_477_320_345"
                            //Restaurants
                        },
                        RestaurantName = new RestaurantName()
                        {
                            Name = "Burger King PL",
                            WebSite = "https://burgerking.pl/pl/",
                            //Restaurants
                        },
                        Location = new Location()
                        {
                            Country = "Poland",
                            City = "Warsaw",
                            Street = "Stokłosy 3"
                        },
                        Reviews = new List<Review>()
                        {
                            new Review(){
                                Title = "BK is the best!", 
                                Text = "I love this so much!!! wow)))",
                                Rating = 10,
                                Reviewer = new Reviewer(){
                                    FirstName = "Patric",
                                    LastName = "Joj"
                                }
                            },
                            new Review(){
                                Title = "BK",
                                Text = "Było spoko",
                                Rating = 7,
                                Reviewer = new Reviewer(){
                                    FirstName = "Maciej",
                                    LastName = "Bobrow"
                                }
                            },
                            new Review(){
                                Title = "Opinia",
                                Text = "Kotlet bardzo mało i sos obrzydliwy",
                                Rating = 2,
                                Reviewer = new Reviewer(){
                                    FirstName = "Marta",
                                    LastName = "Haase"
                                }
                            }
                        }
                    },
                    new Restaurant()
                    {
                        DateOfStart = new DateTime(2015, 1, 3),
                        ContactNumber = "+48_345_886_603",
                        HasDelivery = true,
                        Owner = new Owner()
                        {
                            FirstName = "Robert",
                            LastName = "Sakso",
                            DateOfStart = new DateTime(2017, 7, 7),
                            ContactNumber = "+48_477_311_311"
                            //Restaurants
                        },
                        RestaurantName = new RestaurantName()
                        {
                            Name = "Pizza Hut PL",
                            WebSite = "https://pizzahut.pl",
                            //Restaurants
                        },
                        Location = new Location()
                        {
                            Country = "Poland",
                            City = "Warsaw",
                            Street = "Widok 26"
                        },
                        Reviews = new List<Review>()
                        {
                            new Review(){
                                Title = "Pizza Hut is the best!",
                                Text = "Pizza wow)))",
                                Rating = 9,
                                Reviewer = new Reviewer(){
                                    FirstName = "Patric",
                                    LastName = "Broskwinski"
                                }
                            },
                            new Review(){
                                Title = "BK",
                                Text = "Było spoko",
                                Rating = 10,
                                Reviewer = new Reviewer(){
                                    FirstName = "Maciej",
                                    LastName = "Bobrow"
                                }
                            },
                            new Review(){
                                Title = "Opinia",
                                Text = "Pizza mała i sos obrzydliwy",
                                Rating = 3,
                                Reviewer = new Reviewer(){
                                    FirstName = "Szymon",
                                    LastName = "Haase"
                                }
                            }
                        }
                    },
                    new Restaurant()
                    {
                        DateOfStart = new DateTime(2018, 9, 23),
                        ContactNumber = "+48_050_228_443",
                        HasDelivery = true,
                        Owner = new Owner()
                        {
                            FirstName = "Valentyn",
                            LastName = "Obal",
                            DateOfStart = new DateTime(2018, 9, 23),
                            ContactNumber = "+48_455_322_666"
                            //Restaurants
                        },
                        RestaurantName = new RestaurantName()
                        {
                            Name = "KFC UA",
                            WebSite = "https://www.kfc-ukraine.com",
                            //Restaurants
                        },
                        Location = new Location()
                        {
                            Country = "Ukraine",
                            City = "Lviv",
                            Street = "Prospekt Svobody 19"
                        },
                        Reviews = new List<Review>()
                        {
                            new Review(){
                                Title = "Вау!!!",
                                Text = "Прекрасна жирна їжа)))",
                                Rating = 10,
                                Reviewer = new Reviewer(){
                                    FirstName = "Невідомий",
                                    LastName = "Невідомець"
                                }
                            },
                            new Review(){
                                Title = "КФС",
                                Text = "Як тільки відкрились, то стали моїм улюбленим закладом!",
                                Rating = 7,
                                Reviewer = new Reviewer(){
                                    FirstName = "Роберт",
                                    LastName = "Сафонов"
                                }
                            },
                            new Review(){
                                Title = "Привіт, Я Юра",
                                Text = "Все було гарно. Швидке обслуговування, милий персонал, чистий туалет. Прийду ще)",
                                Rating = 8,
                                Reviewer = new Reviewer(){
                                    FirstName = "Marta",
                                    LastName = "Haase"
                                }
                            }
                        }
                    }
                };
                dataContext.Restaurants.AddRange(restaurants);
                dataContext.SaveChanges();
            }
        }
    }
}
