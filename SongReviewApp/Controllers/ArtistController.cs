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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository artistRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public ArtistController(
            IArtistRepository _artistRepository,
            ICountryRepository _countryRepository,
            IMapper _mapper)
        {
            this.artistRepository = _artistRepository;
            this.countryRepository = _countryRepository;
            this.mapper = _mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ArtistRepository>))]
        public async Task<IActionResult> GetArtists()
        {
            var artists = mapper
                .Map<List<ArtistDto>>(await this.artistRepository.GetArtists());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(artists);
        }

        [HttpGet("{artistId}")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtist(int artistId)
        {
            bool artistExistsCheck = await this.artistRepository.ArtistExists(artistId);
            if (!artistExistsCheck)
            {
                return NotFound();
            }

            var artist = mapper.Map<ArtistDto>(await this.artistRepository.GetArtistById(artistId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(artist);
        }

        [HttpGet("{artistId}/song")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSongByArtist(int artistId)
        {
            bool artistExistsCheck = await this.artistRepository.ArtistExists(artistId);
            if (!artistExistsCheck)
            {
                return NotFound();
            }

            var artist = mapper.Map<ArtistDto>(await this.artistRepository.GetSongByArtist(artistId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(artist);


        }





        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateArtist([FromBody] ArtistDto artistCreate, [FromQuery] int countryId)
        {
            //NOTE: [FromBody] will pull the data from the request body.
            //NOTE: [FromQuery] will pull the data from the url.
            if (artistCreate == null)
            {
                return BadRequest(ModelState);
            }

            var artists = await artistRepository.GetArtists();

            var artist = artists
                .Where(g => g.Name.Trim().ToUpper() == artistCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (artist != null)
            {
                ModelState.AddModelError("", "Artist already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artistMap = mapper.Map<Artist>(artistCreate);
            artistMap.Country = await countryRepository.GetCountryById(countryId);

            var artistCreatedSuccessfully = await artistRepository.CreateArtist(artistMap);

            if (!artistCreatedSuccessfully)
            {
                ModelState.AddModelError("", "Something went wrong while creating new artist.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created artist.");

        }
    }
}
