namespace SongReviewApp.Contracts
{
    using SongReviewApp.Models;

    public interface IReviewRepository
    {
        /// <summary>
        /// Gets reviews.
        /// </summary>
        /// <returns>A collection of reviews.</returns>
        Task<ICollection<Review>> GetReviews();

        /// <summary>
        /// Gets review by id.
        /// </summary>
        /// <param name="reviewId">Review id.</param>
        /// <returns>Review.</returns>
        Task<Review> GetReview(int reviewId);

        /// <summary>
        /// Gets reviews of a song.
        /// </summary>
        /// <param name="songId">Song id.</param>
        /// <returns>A collection of reviews.</returns>
        Task<ICollection<Review>> GetReviewsOfASong(int songId);

        /// <summary>
        /// Checks if review exists.
        /// </summary>
        /// <param name="reviewId">Review id.</param>
        /// <returns>True / False</returns>
        Task<bool> ReviewExists(int reviewId);
    }
}
