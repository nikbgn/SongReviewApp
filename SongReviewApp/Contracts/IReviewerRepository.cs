namespace SongReviewApp.Contracts
{
    using SongReviewApp.Models;

    public interface IReviewerRepository
    {
        /// <summary>
        /// Gets reviewers.
        /// </summary>
        /// <returns>A collection of reviewers.</returns>
        Task<ICollection<Reviewer>> GetReviewers();

        /// <summary>
        /// Gets reviewer by id.
        /// </summary>
        /// <param name="reviewerId">Reviewer id.</param>
        /// <returns>Reviewer.</returns>
        Task<Reviewer> GetReviewer(int reviewerId);

        /// <summary>
        /// Gets reviews of a reviewer.
        /// </summary>
        /// <param name="reviewerId">Reviewer id.</param>
        /// <returns>A collection of reviews.</returns>
        Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId);

        /// <summary>
        /// Checks if reviewer exists.
        /// </summary>
        /// <param name="reviewerId">Reviewer id.</param>
        /// <returns>True / False</returns>
        Task<bool> ReviewerExists(int reviewerId);
    }
}
