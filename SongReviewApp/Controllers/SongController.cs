namespace SongReviewApp.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using SongReviewApp.Contracts;
    using SongReviewApp.Dto;
    using SongReviewApp.Models;
    using SongReviewApp.Repository;

    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository songRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public SongController(
            ISongRepository _songRepository,
            IReviewRepository _reviewRepository,
            IMapper _mapper)
        {
            this.songRepository = _songRepository;
            this.reviewRepository = _reviewRepository;
            this.mapper = _mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SongRepository>))]
        public async Task<IActionResult> GetSongs()
        {
            var songs = mapper
                .Map<List<SongDto>>(await this.songRepository.GetSongs());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(songs);
        }

        [HttpGet("{songId}")]
        [ProducesResponseType(200, Type = typeof(Song))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSong(int songId)
        {
            bool songExistsCheck = await this.songRepository.SongExists(songId);
            if (!songExistsCheck)
            {
                return NotFound();
            }

            var song = mapper.Map<SongDto>(await this.songRepository.GetSongById(songId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(song);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateSong([FromBody] SongDto songCreate, [FromQuery] int artistId, [FromQuery] int genreId)
        {
            //NOTE: [FromBody] will pull the data from the request body.
            //NOTE: [FromQuery] will pull the data from the url.
            if (songCreate == null)
            {
                return BadRequest(ModelState);
            }

            var songsCollection = await songRepository.GetSongs();

            var song = songsCollection
                .Where(g => g.Name.Trim().ToUpper() == songCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (song != null)
            {
                ModelState.AddModelError("", "Song already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var songMap = mapper.Map<Song>(songCreate);
           


            var artistCreatedSuccessfully = await songRepository.CreateSong(artistId, genreId, songMap);

            if (!artistCreatedSuccessfully)
            {
                ModelState.AddModelError("", "Something went wrong while creating new song.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created song.");

        }

        [HttpPut("{songId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateSong(
            int songId, 
            [FromQuery] int artistId, 
            [FromQuery] int genreId,
            [FromBody] SongDto updatedSong)
        {
            if (updatedSong == null) return BadRequest(ModelState);

            if (artistId != updatedSong.Id) return BadRequest(ModelState);

            var songExists = await songRepository.SongExists(artistId);

            if (!songExists) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var songMap = mapper.Map<Song>(updatedSong);

            var artistUpdated = await songRepository.UpdateSong(artistId,genreId, songMap);

            if (!artistUpdated)
            {
                ModelState.AddModelError("", "Something went wrong.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
