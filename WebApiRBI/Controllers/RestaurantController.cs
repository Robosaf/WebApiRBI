using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiRBI.Dto;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IRestaurantNameRepository _restaurantNameRepository;
        private readonly IReviewRepository _reviewRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository,
            IMapper mapper, ILocationRepository locationRepository,
            IOwnerRepository ownerRepository,
            IRestaurantNameRepository restaurantNameRepository,
            IReviewRepository reviewRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _ownerRepository = ownerRepository;
            _restaurantNameRepository = restaurantNameRepository;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        public IActionResult GetRestaurants()
        {
            var restaurants = _mapper.Map<List<RestaurantDto>>(_restaurantRepository.GetRestaurants());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(restaurants);
        }

        [HttpGet("{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(Restaurant))]
        [ProducesResponseType(400)]
        public IActionResult GetRestaurant(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var restaurant = _mapper.Map<RestaurantDto>(_restaurantRepository.GetRestaurant(restaurantId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurant);
        }

        [HttpGet("{restaurantId}/rating")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetRestaurantRating(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var rating = _restaurantRepository.GetRestaurantRating(restaurantId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpGet("byCity/{city}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetRestaurantsByCity(string city)
        {
            if (!_locationRepository.CityExist(city))
                return NotFound();

            var restaurants = _mapper.Map<List<RestaurantDto>>(_restaurantRepository.GetRestaurantsByCity(city));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(restaurants);
        }


        [HttpGet("byCountry/{country}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetRestaurantsByCountry(string country)
        {
            if (!_locationRepository.CountryExist(country))
                return NotFound();

            var restaurants = _mapper.Map<List<RestaurantDto>>(_restaurantRepository.GetRestaurantsByCountry(country));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(restaurants);
        }

        [HttpGet("ownerByRest/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetOwnerByRestaurantId(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_restaurantRepository.GetOwnerByRestaurantId(restaurantId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRestaurant([FromQuery] int ownerId, [FromQuery] int restNameId,
            [FromQuery] int locationId, [FromBody] RestaurantDto restaurantCreate)
        {
            if (restaurantCreate == null)
                return BadRequest();

            var restaurant = _restaurantRepository.GetRestaurants()
                .Where(r => r.ContactNumber.Trim().ToUpper() == restaurantCreate.ContactNumber.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (restaurant != null)
            {
                ModelState.AddModelError("", "Restaurant already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantMap = _mapper.Map<Restaurant>(restaurantCreate);

            restaurantMap.Owner = _ownerRepository.GetOwner(ownerId);
            restaurantMap.RestaurantName = _restaurantNameRepository.GetRestName(restNameId);
            restaurantMap.Location = _locationRepository.GetLocation(locationId);

            if (!_restaurantRepository.CreateRestaurant(restaurantMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{restaurantId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRestaurant(int restaurantId, [FromBody] RestaurantDto updatedRestaurant)
        {
            if (updatedRestaurant == null)
                return BadRequest(ModelState);

            if (restaurantId != updatedRestaurant.Id)
                return BadRequest(ModelState);

            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var restaurantMap = _mapper.Map<Restaurant>(updatedRestaurant);

            if (!_restaurantRepository.UpdateRestaurant(restaurantMap))
            {
                ModelState.AddModelError("", "Something went wrong updating restaurant");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //Delete Method
        [HttpDelete("{restaurantId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRestaurant(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var restaurantToDelete = _restaurantRepository.GetRestaurant(restaurantId);

            var reviewsToDelete = _reviewRepository.GetReviewsByRestaurantId(restaurantId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete))
                ModelState.AddModelError("", "Something went wrong deleting reviews");

            if (!_restaurantRepository.DeleteRestaurant(restaurantToDelete))
                ModelState.AddModelError("", "Something went wrong deleting restaurant");

            return NoContent();
        }
    }
}
