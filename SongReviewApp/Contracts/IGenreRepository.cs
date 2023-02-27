namespace SongReviewApp.Contracts
{
    using SongReviewApp.Models;

    public interface IGenreRepository
    {
        /// <summary>
        /// Gets genres
        /// </summary>
        /// <returns>Collection of genres</returns>
        Task<ICollection<Genre>> GetGenres();

        /// <summary>
        /// Gets genre by id
        /// </summary>
        /// <param name="id">Gendre identifier</param>
        /// <returns>Genre</returns>
        Task<Genre> GetGenreById(int id);

        /// <summary>
        /// Gets song by genre
        /// </summary>
        /// <param name="genreId">Genre id</param>
        /// <returns>Collection of songs</returns>
        Task<ICollection<Song>> GetSongByGenre(int genreId);

        /// <summary>
        /// Checks if a genre exists by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>True / False</returns>
        Task<bool> GenreExists(int id);
    }
}
