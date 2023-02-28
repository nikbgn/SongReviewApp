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
        private readonly IMapper mapper;

        public SongController(
            ISongRepository _songRepository,
            IMapper _mapper)
        {
            this.songRepository = _songRepository;
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



    }
}
