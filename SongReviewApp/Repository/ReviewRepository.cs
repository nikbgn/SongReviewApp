﻿namespace SongReviewApp.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using SongReviewApp.Contracts;
    using SongReviewApp.Data;
    using SongReviewApp.Models;

    public class ReviewRepository : IReviewRepository
    {

        private readonly ApplicationDbContext dbContext;

        public ReviewRepository(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<Review> GetReview(int reviewId)
        {
            try
            {
                var result = await dbContext.Reviews
                    .Where(r => r.Id == reviewId)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return result;
                }

                throw new ArgumentException("No review with that ID exists.");
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Review>> GetReviews()
        {
            try
            {
                var result = await dbContext.Reviews
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Review>> GetReviewsOfASong(int songId)
        {
            try
            {
                var result = await dbContext.Reviews
                    .Where(r => r.Song.Id == songId)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<bool> ReviewExists(int reviewId)
        {
            try
            {
                var result = await dbContext.Reviews
                    .AnyAsync(r => r.Id == reviewId);

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