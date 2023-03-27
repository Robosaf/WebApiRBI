using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiRBI.Dto;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantNameController : Controller
    {
        private readonly IRestaurantNameRepository _restaurantNameRepository;
        private readonly IMapper _mapper;

        public RestaurantNameController(IRestaurantNameRepository restaurantNameRepository, IMapper mapper)
        {
            _restaurantNameRepository = restaurantNameRepository;
            _mapper = mapper;
        }

        [HttpGet("{nameId}")]
        [ProducesResponseType(200, Type = typeof(RestaurantName))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetRestName(int nameId)
        {
            if (!_restaurantNameRepository.RestNameExist(nameId))
                return NotFound();

            var restaurantName
                = _mapper.Map<RestaurantNameDto>(_restaurantNameRepository.GetRestName(nameId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(restaurantName);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantName>))]
        [ProducesResponseType(400)]
        public IActionResult GetRestNames()
        {
            var restNames = _mapper.Map<List<RestaurantNameDto>>(_restaurantNameRepository.GetRestNames());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(restNames);
        }

        [HttpGet("name")]
        [ProducesResponseType(200, Type = typeof(RestaurantName))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetRestNameByName(string name)
        {
            if (!_restaurantNameRepository.RestNameExist(name))
                return NotFound();

            var restName = _mapper.Map<RestaurantNameDto>(_restaurantNameRepository.GetRestNameByName(name));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(restName);
        }

        //Post Method
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRestaurantName([FromBody] RestaurantNameDto restNameCreate)
        {
            //check on NULL
            if (restNameCreate == null)
                return BadRequest();

            //check if the same is entity in data
            var restName = _restaurantNameRepository.GetRestNames()
                .Where(rn => rn.Name.Trim().ToUpper() == restNameCreate.Name.TrimEnd().ToUpper())
                .LastOrDefault();

            //if YES -> ERROR
            if (restName != null)
            {
                ModelState.AddModelError("", "Restaurant name already exist");
                return StatusCode(422, ModelState);
            }

            // Check if the values were able to bind correctly to the model && 
            // no validation rules were broken in the process.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //mapping a value
            var restNameMap = _mapper.Map<RestaurantName>(restNameCreate);

            //creating a mapped Value
            if (!_restaurantNameRepository.CreateRestaurantName(restNameMap))
            {
                ModelState.AddModelError("", "Something wrong while saving");
                return StatusCode(500, ModelState);
            }

            //returning that the all was OK
            return Ok("Succesfully created");
        }

        //Update Method
        [HttpPut("{restNameId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRestaurantName(int restNameId, RestaurantNameDto updatedRestName)
        {
            if(updatedRestName == null)
                return BadRequest(ModelState);

            if (restNameId != updatedRestName.Id)
                return BadRequest(ModelState);

            if (!_restaurantNameRepository.RestNameExist(restNameId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var restNameMap = _mapper.Map<RestaurantName>(updatedRestName);

            if(!_restaurantNameRepository.UpdateRestaurantName(restNameMap))
            {
                ModelState.AddModelError("", "Something went wrong updating restaurant name");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //Delete Method
        [HttpDelete("{restNameId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRestaurantName(int restNameId)
        {
            if (!_restaurantNameRepository.RestNameExist(restNameId))
                return NotFound();

            var restNameToDelete = _restaurantNameRepository.GetRestName(restNameId);

            if (!_restaurantNameRepository.DeleteRestaurantName(restNameToDelete))
                ModelState.AddModelError("", "Something went wrong deleting restaurant name");

            return NoContent();
        }
    }
}
