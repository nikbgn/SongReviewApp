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


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDto countryCreate)
        {
            //NOTE: [FromBody] will pull the data from the request body.
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var countries = await countryRepository.GetCountries();

            var country = countries
                .Where(g => g.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = mapper.Map<Country>(countryCreate);

            var countryCreatedSuccessfully = await countryRepository.CreateCountry(countryMap);

            if (!countryCreatedSuccessfully)
            {
                ModelState.AddModelError("", "Something went wrong while creating new country.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created genre.");

        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null) return BadRequest(ModelState);

            if (countryId != updatedCountry.Id) return BadRequest(ModelState);

            var countryExists = await countryRepository.CountryExists(countryId);

            if (!countryExists) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var countryMap = mapper.Map<Country>(updatedCountry);

            var countryUpdated = await countryRepository.UpdateCountry(countryMap);

            if (!countryUpdated)
            {
                ModelState.AddModelError("", "Something went wrong.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
