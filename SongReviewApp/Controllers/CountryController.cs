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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public CountryController(
            ICountryRepository _countryRepository,
            IMapper _mapper)
        {
            this.countryRepository = _countryRepository;
            this.mapper = _mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public async Task<IActionResult> GetCountries()
        {
            var countries = mapper
                .Map<List<CountryDto>>(await this.countryRepository.GetCountries());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(countries);
        }


        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountry(int countryId)
        {
            bool countryExistsCheck = await this.countryRepository.CountryExists(countryId);
            if (!countryExistsCheck)
            {
                return NotFound();
            }

            var country = mapper.Map<CountryDto>(await this.countryRepository.GetCountryById(countryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [HttpGet("artists/{artistId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountryOfAnArtist(int artistId)
        {
            var country = mapper
                .Map<CountryDto>(await countryRepository.GetCountryByArtist(artistId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(country);
        }

    }
}
