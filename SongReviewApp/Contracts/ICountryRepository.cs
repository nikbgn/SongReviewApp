namespace SongReviewApp.Contracts
{
    using SongReviewApp.Models;

    public interface ICountryRepository
    {
        /// <summary>
        /// Gets countries.
        /// </summary>
        /// <returns>Collection of countries</returns>
        Task<ICollection<Country>> GetCountries();

        /// <summary>
        /// Gets country by id.
        /// </summary>
        /// <param name="id">Country id.</param>
        /// <returns>Country.</returns>
        Task<Country> GetCountryById(int id);

        /// <summary>
        /// Gets country by artist.
        /// </summary>
        /// <param name="artistId">Artist id.</param>
        /// <returns>Country</returns>
        Task<Country> GetCountryByArtist(int artistId);

        /// <summary>
        /// Gets artists from a country.
        /// </summary>
        /// <param name="countryId">Country id.</param>
        /// <returns>Collection of artists.</returns>
        Task<ICollection<Artist>> GetArtistsFromCountry(int countryId);

        /// <summary>
        /// Checks if country exists.
        /// </summary>
        /// <param name="id">Country id.</param>
        /// <returns>True / False</returns>
        Task<bool> CountryExists(int id);
    }
}
