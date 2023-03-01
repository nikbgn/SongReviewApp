namespace SongReviewApp.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SongReviewApp.Contracts;
    using SongReviewApp.Data;
    using SongReviewApp.Models;

    public class GenreRepository : IGenreRepository, ICommonDbOperations
    {

        private readonly ApplicationDbContext dbContext;

        public GenreRepository(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<bool> CreateGenre(Genre genre)
        {
            try
            {
                await dbContext.AddAsync(genre);
                bool saveSuccess = await SaveChangesAsync();
                return saveSuccess;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<bool> GenreExists(int id)
        {
            try
            {
                var result = await dbContext.Genres
                    .AnyAsync(g => g.Id == id);

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<Genre> GetGenreById(int id)
        {
            try
            {
                var result = await dbContext.Genres
                    .Where(g => g.Id == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return result;
                }

                throw new ArgumentException("No genre with that ID exists.");
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            try
            {
                var result = await dbContext.Genres
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Song>> GetSongByGenre(int genreId)
        {
            try
            {
                var result = await dbContext.SongGenres
                    .Where(s => s.GenreId == genreId)
                    .Select(g => g.Song)
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
    }
}
