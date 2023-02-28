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
        private readonly IMapper mapper;

        public ArtistController(
            IArtistRepository _artistRepository,
            IMapper _mapper)
        {
            this.artistRepository = _artistRepository;
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
    }
}
