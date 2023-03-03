namespace SongReviewApp.Repository
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using SongReviewApp.Contracts;
    using SongReviewApp.Data;
    using SongReviewApp.Models;

    public class SongRepository : ISongRepository, ICommonDbOperations
    {
        private readonly ApplicationDbContext dbContext;

        public SongRepository(ApplicationDbContext _dbContext)
        {
            this.dbContext   = _dbContext;
        }

        public async Task<bool> CreateSong(int artistId, int genreId, Song song)
        {
            try
            {
                var songOwner = await dbContext.Artists.Where(a => a.Id == artistId).FirstOrDefaultAsync();
                var genre = await dbContext.Genres.Where(g => g.Id == genreId).FirstOrDefaultAsync();

                if(songOwner == null || genre == null)
                {
                    throw new ArgumentException("Invalid artist or genre.");
                }

                var songArtist = new SongArtist()
                {
                    Artist = songOwner,
                    Song = song
                };

                await dbContext.AddAsync(songArtist);

                var songGenre = new SongGenre()
                {
                    Genre = genre,
                    Song = song
                };

                await dbContext.AddAsync(songGenre);
                await dbContext.AddAsync(song);

                var savedSuccessfully = await SaveChangesAsync();

                return savedSuccessfully;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<Song> GetSongById(int id)
        {
            try
            {
                var result = await dbContext.Songs
                    .Where(s => s.Id == id)
                    .FirstOrDefaultAsync();

                if(result != null)
                {
                    return result;
                }

                throw new ArgumentException("No song with that ID exists.");
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<Song> GetSongByName(string name)
        {
            try
            {
                var result = await dbContext.Songs
                    .Where(s => s.Name == name)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return result;
                }

                throw new ArgumentException("No song with that name exists.");
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Song>> GetSongs()
        {
            try
            {
                var result = await dbContext.Songs
                    .OrderBy(s => s.Id)
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

        public async Task<bool> SongExists(int id)
        {
            try
            {
                var result = await dbContext.Songs
                    .AnyAsync(s => s.Id == id);

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<bool> UpdateSong(int artistId, int genreId, Song song)
        {
            dbContext.Update(song);
            return await SaveChangesAsync();
        }
    }
}
  