namespace SongReviewApp.Contracts
{
    public interface ICommonDbOperations
    {
        /// <summary>
        /// Saves changes.
        /// </summary>

        Task<bool> SaveChangesAsync();
    }
}
