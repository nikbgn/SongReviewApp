namespace SongReviewApp.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using SongReviewApp.Contracts;
    using SongReviewApp.Dto;
    using SongReviewApp.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
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

    }
}
