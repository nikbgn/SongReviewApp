namespace SongReviewApp.Repository
{
    using System.Collections.Generic;
    using System.Diagnostics.Metrics;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using SongReviewApp.Contracts;
    using SongReviewApp.Data;
    using SongReviewApp.Models;

    public class ArtistRepository : IArtistRepository, ICommonDbOperations
    {

        private readonly ApplicationDbContext dbContext;

        public ArtistRepository(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }


        public async Task<bool> ArtistExists(int artistId)
        {
            try
            {
                var result = await dbContext.Artists
                    .AnyAsync(a => a.Id == artistId);

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<bool> CreateArtist(Artist artist)
        {
            try
            {
                await dbContext.AddAsync(artist);
                bool saveSuccess = await SaveChangesAsync();
                return saveSuccess;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<Artist> GetArtistById(int artistId)
        {
            try
            {
                var result = await dbContext.Artists
                    .Where(a => a.Id == artistId)
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

        public async Task<ICollection<Artist>> GetArtistOfASong(int songId)
        {
            try
            {
                var result = await dbContext.SongArtists
                    .Where(s => s.Song.Id == songId)
                    .Select(a => a.Artist)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Artist>> GetArtists()
        {
            try
            {
                var result = await dbContext.Artists
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Song>> GetSongByArtist(int artistId)
        {
            try
            {
                var result = await dbContext.SongArtists
                    .Where(a => a.Artist.Id == artistId)
                    .Select(s => s.Song)
                    .ToListAsync();

                return result;
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

        public async Task<bool> UpdateArtist(Artist artist)
        {
            dbContext.Update(artist);
            return await SaveChangesAsync();
        }
    }
}
