using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiRBI.Dto;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;

        public LocationController(ILocationRepository locationRepository, IMapper mapper,
            IRestaurantRepository restaurantRepository)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(200, Type = typeof(Location))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetLocation(int locationId)
        {

            if (!_locationRepository.LocationExist(locationId))
                return NotFound();

            var location = _mapper.Map<LocationDto>(_locationRepository.GetLocation(locationId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(location);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]
        [ProducesResponseType(400)]
        public IActionResult GetLocations()
        {
            var locations = _mapper.Map<List<LocationDto>>(_locationRepository.GetLocations());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(locations);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLocation([FromBody] LocationDto locationCreate)
        {
            if (locationCreate == null)
                return BadRequest();

            var location = _locationRepository.GetLocations()
                .Where(l => l.City.Trim().ToUpper() == locationCreate.City.TrimEnd().ToUpper()
                && l.Street.Trim().ToUpper() == locationCreate.Street.TrimEnd().ToUpper()).FirstOrDefault();

            if (location != null)
            {
                ModelState.AddModelError("", "Location already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationMap = _mapper.Map<Location>(locationCreate);

            if (!_locationRepository.CreateLocation(locationMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }
        //Update Method
        [HttpPut("{locationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLocation(int locationId, [FromBody] LocationDto updatedLocation)
        {
            if (updatedLocation == null)
                return BadRequest(ModelState);

            if (locationId != updatedLocation.Id)
                return BadRequest(ModelState);

            if (!_locationRepository.LocationExist(locationId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var locationMap = _mapper.Map<Location>(updatedLocation);

            if (!_locationRepository.UpdateLocation(locationMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }

        //Delete Method
        [HttpDelete("{locationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLocation(int locationId)
        {
            if (!_locationRepository.LocationExist(locationId))
                return NotFound();

            var locationToDelete = _locationRepository.GetLocation(locationId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_locationRepository.DeleteLocation(locationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting location");
            }

            return NoContent();
        }
    }
}
