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
        private readonly ISongRepository songRepository;
        private readonly IReviewerRepository reviewerRepository;
        private readonly IMapper mapper;

        public ReviewController(
            IReviewRepository _reviewRepository,
            ISongRepository _songRepository,
            IReviewerRepository _reviewerRepository,
            IMapper _mapper)
        {
            this.reviewRepository = _reviewRepository;
            this.songRepository = _songRepository;
            this.reviewerRepository = _reviewerRepository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewCreate, [FromQuery] int reviewerId, [FromQuery] int songId)
        {
            if (reviewCreate == null)
            {
                return BadRequest(ModelState);
            }

            var reviewsCollection = await reviewRepository.GetReviews();

            var review = reviewsCollection
                .Where(r => r.Id == reviewCreate.Id)
                .FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = mapper.Map<Review>(reviewCreate);

            reviewMap.Song = await songRepository.GetSongById(songId);
            reviewMap.Reviewer = await reviewerRepository.GetReviewer(reviewerId);

            var artistCreatedSuccessfully = await reviewRepository.CreateReview(reviewMap);

            if (!artistCreatedSuccessfully)
            {
                ModelState.AddModelError("", "Something went wrong while creating new review.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created review.");

        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null) return BadRequest(ModelState);

            if (reviewId != updatedReview.Id) return BadRequest(ModelState);

            var reviewExists = await reviewRepository.ReviewExists(reviewId);

            if (!reviewExists) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var reviewMap = mapper.Map<Review>(updatedReview);

            var reviewUpdated = await reviewRepository.UpdateReview(reviewMap);

            if (!reviewUpdated)
            {
                ModelState.AddModelError("", "Something went wrong.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
