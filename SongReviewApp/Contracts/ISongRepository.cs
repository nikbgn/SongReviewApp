namespace SongReviewApp.Contracts
{
    using SongReviewApp.Models;

    public interface ISongRepository
    {
        Task<ICollection<Song>> GetSongs();
    }
}
