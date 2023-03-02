namespace SongReviewApp.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using SongReviewApp.Contracts;
    using SongReviewApp.Data;
    using SongReviewApp.Models;

    public class ReviewerRepository : IReviewerRepository, ICommonDbOperations
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewerRepository(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<bool> CreateReviewer(Reviewer reviewer)
        {
            try
            {
                await dbContext.AddAsync(reviewer);
                return await SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<Reviewer> GetReviewer(int reviewerId)
        {
            try
            {
                var result = await dbContext.Reviewers
                    .Where(r => r.Id == reviewerId)
                    .Include(e => e.Reviews)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Reviewer>> GetReviewers()
        {
            try
            {
                var result = await dbContext.Reviewers
                    .Include(e => e.Reviews)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId)
        {
            try
            {
                var result = await dbContext.Reviews
                    .Where(r => r.Reviewer.Id == reviewerId)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Implement exception logic, add logging?
                throw;
            }
        }

        public async Task<bool> ReviewerExists(int reviewerId)
        {
            try
            {
                var result = await dbContext.Reviewers
                    .AnyAsync(r => r.Id == reviewerId);

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
