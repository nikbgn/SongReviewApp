namespace SongReviewApp.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using SongReviewApp.Contracts;
    using SongReviewApp.Dto;
    using SongReviewApp.Models;


    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;

        public GenreController(
            IGenreRepository _genreRepository,
            IMapper _mapper)
        {
            this.genreRepository = _genreRepository;
            this.mapper = _mapper;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public async Task<IActionResult> GetGenres()
        {
            var songs = mapper
                .Map<List<GenreDto>>(await this.genreRepository.GetGenres());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(songs);
        }



        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGenre(int genreId)
        {
            bool genreExistsCheck = await this.genreRepository.GenreExists(genreId);
            if (!genreExistsCheck)
            {
                return NotFound();
            }

            var genre = mapper.Map<GenreDto>(await this.genreRepository.GetGenreById(genreId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(genre);
        }

        [HttpGet("song/{genreId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Song>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSongByGenreId(int genreId)
        {
            var songs = mapper
                .Map<List<SongDto>>(await genreRepository.GetSongByGenre(genreId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(songs);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGenre([FromBody]GenreDto genreCreate)
        {
            //NOTE: [FromBody] will pull the data from the request body.
            if (genreCreate == null)
            {
                return BadRequest(ModelState);
            }

            var genres = await genreRepository.GetGenres();

            var genre = genres
                .Where(g => g.Name.Trim().ToUpper() == genreCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if(genre != null)
            {
                ModelState.AddModelError("", "Genre already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genreMap = mapper.Map<Genre>(genreCreate);

            var genreCreatedSuccessfully = await genreRepository.CreateGenre(genreMap);

            if (!genreCreatedSuccessfully)
            {
                ModelState.AddModelError("", "Something went wrong while creating new genre.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created genre.");

        }

    }
}
