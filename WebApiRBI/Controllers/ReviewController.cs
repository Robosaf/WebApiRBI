using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiRBI.Dto;
using WebApiRBI.Interfaces;
using WebApiRBI.Models;

namespace WebApiRBI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public ReviewController(IMapper mapper, IReviewRepository reviewRepository,
            IReviewerRepository reviewerRepository, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExist(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(review);
        }

        [HttpGet("byReviewer")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewByReviewer([FromQuery] string firstname, [FromQuery] string lastname)
        {
            if (!_reviewerRepository.ReviewerExist(firstname, lastname))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReviewByReviewer(firstname, lastname));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(review);
        }

        //Post Method
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId,
            [FromQuery] int restaurantId, [FromBody] ReviewDto reviewCreate)
        {
            if (restaurantId == 0 && reviewerId == 0 && reviewCreate == null)
                return BadRequest();

            var review = _reviewRepository.GetReviews()
                .Where(rev => rev.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper())
                .FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Restaurant = _restaurantRepository.GetRestaurant(restaurantId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);

            if(!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        //Update Method
        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExist(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //Delete Method
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExist(reviewId))
                return NotFound();

            var reviewToDelete = _reviewRepository.GetReview(reviewId);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
                ModelState.AddModelError("", "Something went wrong deleting review");

            return NoContent();
        }
    }
}
