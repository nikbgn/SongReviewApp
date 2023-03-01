namespace SongReviewApp.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using SongReviewApp.Contracts;
    using SongReviewApp.Data;
    using SongReviewApp.Models;

    public class CountryRepository : ICountryRepository, ICommonDbOperations
    {

        private readonly ApplicationDbContext dbContext;

        public CountryRepository(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }


        public async Task<bool> CountryExists(int id)
        {
            try
            {
                var result = await dbContext.Countries
                    .AnyAsync(c => c.Id == id);

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<bool> CreateCountry(Country country)
        {
            try
            {
                await dbContext.AddAsync(country);
                bool saveSuccess = await SaveChangesAsync();
                return saveSuccess;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Artist>> GetArtistsFromCountry(int countryId)
        {
            try
            {
                var result = await dbContext.Artists
                    .Where(c => c.Country.Id == countryId)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Country>> GetCountries()
        {
            try
            {
                var result = await dbContext.Countries
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<Country> GetCountryByArtist(int artistId)
        {
            try
            {
                var result = await dbContext.Artists
                    .Where(a => a.Id == artistId)
                    .Select(c => c.Country)
                    .FirstOrDefaultAsync();


                if (result != null)
                {
                    return result;
                }

                throw new ArgumentException("No artist with that ID exists.");
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<Country> GetCountryById(int id)
        {
            try
            {
                var result = await dbContext.Countries
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return result;
                }

                throw new ArgumentException("No country with that ID exists.");
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            int saved = await dbContext.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
