namespace SongReviewApp.Repository
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using SongReviewApp.Contracts;
    using SongReviewApp.Data;
    using SongReviewApp.Models;

    public class SongRepository : ISongRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SongRepository(ApplicationDbContext _dbContext)
        {
            this.dbContext   = _dbContext;
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
    }
}
