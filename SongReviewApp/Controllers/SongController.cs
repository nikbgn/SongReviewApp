namespace SongReviewApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SongReviewApp.Contracts;
    using SongReviewApp.Repository;

    [Route("api/[controller]")]
    [ApiController]
    public class SongController : Controller
    {
        private readonly ISongRepository songRepository;
        public SongController(ISongRepository _songRepository)
        {
            this.songRepository = _songRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SongRepository>))]
        public async Task<IActionResult> GetSongs()
        {
            var songs = await this.songRepository.GetSongs();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(songs);
        }
    }
}
