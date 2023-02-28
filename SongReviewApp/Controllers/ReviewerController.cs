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
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository reviewerRepository;
        private readonly IMapper mapper;

        public ReviewerController(
            IReviewerRepository _reviewerRepository,
            IMapper _mapper)
        {
            this.reviewerRepository = _reviewerRepository;
            this.mapper = _mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public async Task<IActionResult> GetReviewers()
        {
            var reviewers = mapper
                .Map<List<ReviewerDto>>(await this.reviewerRepository.GetReviewers());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviewers);
        }



        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewer(int reviewerId)
        {
            bool reviewerExistsCheck = await this.reviewerRepository.ReviewerExists(reviewerId);
            if (!reviewerExistsCheck)
            {
                return NotFound();
            }

            var reviewer = mapper.Map<ReviewerDto>(await this.reviewerRepository.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviewer);
        }


        [HttpGet("{reviewerId}/reviews")]
        public async Task<IActionResult> GetReviewsByAReviewer(int reviewerId)
        {
            bool reviewerExistsCheck = await this.reviewerRepository.ReviewerExists(reviewerId);
            if (!reviewerExistsCheck)
            {
                return NotFound();
            }

            var reviews = mapper.Map<List<ReviewDto>>(await this.reviewerRepository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }
    }



}
