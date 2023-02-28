namespace SongReviewApp.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SongReviewApp.Contracts;
    using SongReviewApp.Dto;
    using SongReviewApp.Models;
    using SongReviewApp.Repository;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public ReviewController(
            IReviewRepository _reviewRepository,
            IMapper _mapper)
        {
            this.reviewRepository = _reviewRepository;
            this.mapper = _mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewRepository>))]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = mapper
                .Map<List<ReviewDto>>(await this.reviewRepository.GetReviews());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }


        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReview(int reviewId)
        {
            bool reviewExistsCheck = await this.reviewRepository.ReviewExists(reviewId);
            if (!reviewExistsCheck)
            {
                return NotFound();
            }

            var review = mapper.Map<ReviewDto>(await this.reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(review);
        }

        [HttpGet("song/{songId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsForASong(int songId)
        {
            var reviews = mapper.Map<List<ReviewDto>>(await this.reviewRepository.GetReviewsOfASong(songId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }
    }
}
